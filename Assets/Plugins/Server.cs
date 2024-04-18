using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

/*0-18 = Attacks
 90 = Received attack
 */

public class Server : MonoBehaviour
{
    // Import the setupServer function
    [DllImport("server.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern void setupServer(string ipAddress, string port);

    // Import the sendMessageToClient function
    [DllImport("server.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern void sendMessageToClient(string msg);

    // Import the receiveMessageFromClient function
    [DllImport("server.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern int receiveMessageFromClient();

    // Import the closeConnection function
    [DllImport("server.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern void closeConnection();
    // Start is called before the first frame update

    public UnityEvent onSendSuccessful;
    public UnityEvent onReceiveSuccessful;
    [HideInInspector]
    public int receivedValue;
    private int _port;

    public IEnumerator SendThread(int attackIndex)
    {
        var task = Task.Run(() => SendToClient(attackIndex));
        
        while (!task.IsCompleted)
        {
            Debug.Log($"Thread: {task.Status}");
            yield return new WaitForSeconds(2.0f);
        }
        Debug.Log($"Thread: {task.Status}");
        onSendSuccessful.Invoke();
    }

    public IEnumerator SendMessageToServer(string msg)
    {
        var task = Task.Run(() => SendToClientUDP(msg));

        while (!task.IsCompleted)
        {
            yield return new WaitForSeconds(2.0f);
        }
        onSendSuccessful.Invoke();
    }

    public IEnumerator ExpectMessageFromServer(int expected)
    {
        var task = Task.Run(() => ReceiveFromClientUDP());

        while (!task.IsCompleted)
        {
            Debug.Log($"Thread: {task.Status}");
            yield return new WaitForSeconds(2.0f);
        }
        Debug.Log($"Thread: {task.Status}");
        if(receivedValue == expected)
            onReceiveSuccessful.Invoke();
    }

    public IEnumerator ReceiveThread()
    {
        var task = Task.Run(() => ReceiveFromClient());
        //var stateManager = gameObject.GetComponent<>();

        while (!task.IsCompleted)
        {
            Debug.Log($"Thread: {task.Status}");
            yield return new WaitForSeconds(2.0f);
        }
        Debug.Log($"Thread: {task.Status}");   
        onReceiveSuccessful.Invoke();
    }
    
    private void SendToClient(int attackIndex)
    {
        setupServer("127.0.0.1", "8080");
        
        // Call the sendMessageToClient function
        sendMessageToClient(attackIndex.ToString());
        closeConnection();
    }

    private void SendToClientUDP(string msg)
    {
        setupServer("127.0.0.1", "8080");
        
        // Call the sendMessageToClient function
        sendMessageToClient(msg);
        closeConnection();
    }

    private void ReceiveFromClient()
    {
        setupServer("127.0.0.1", "8080");
        
        // Call the receiveMessageFromClient function
        receivedValue = receiveMessageFromClient();
        closeConnection();
    }

    private void ReceiveFromClientUDP()
    {
        setupServer("127.0.0.1", "8080");
        
        // Call the receiveMessageFromClient function
        receivedValue = receiveMessageFromClient();
        Debug.Log("Received value: " + receivedValue);
        closeConnection();
    }

    public void Close()
    {
        closeConnection();
        Debug.Log("Closed");
    }
}
