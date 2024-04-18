using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

/*0-18 = Attacks
 90 = Received attack
 */

public class Client : MonoBehaviour
{
    // Import the setupServer function
    [DllImport("client.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern void setupClient(string ipAddress, string port);

    // Import the sendMessageToClient function
    [DllImport("client.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern void sendMessageToServer(string msg);

    // Import the receiveMessageFromClient function
    [DllImport("client.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern int recieveMessageFromServer();

    // Import the closeConnection function
    [DllImport("client.dll")] // Change "yourLibraryName" to the name of your C library
    public static extern void closeConnection();
    // Start is called before the first frame update

    public UnityEvent onSendSuccessful;
    public UnityEvent onReceiveSuccessful;
    [HideInInspector]
    public int receivedValue;

    public IEnumerator SendThread(int attackIndex)
    {
        var task = Task.Run(() => SendToClient(attackIndex));
        
        while (!task.IsCompleted)
        {
            Debug.Log($"Send Thread: {task.Status}");
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
            Debug.Log($"Thread: {task.Status}");
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
        setupClient("127.0.0.1", "8080");
        
        // Call the sendMessageToClient function
        sendMessageToServer(attackIndex.ToString());
        var clientConfirmation = recieveMessageFromServer();
        Debug.Log($"Received: {clientConfirmation}");
        closeConnection();
    }

    private void SendToClientUDP(string msg)
    {
        setupClient("127.0.0.1", "8080");
        
        // Call the sendMessageToClient function
        sendMessageToServer(msg);
        closeConnection();
    }

    private void ReceiveFromClient()
    {
        setupClient("127.0.0.1", "8080");
        
        // Call the receiveMessageFromClient function
        receivedValue = recieveMessageFromServer();
        Debug.Log("Received value: " + receivedValue);
        sendMessageToServer("90");
        closeConnection();
    }

    private void ReceiveFromClientUDP()
    {
        setupClient("127.0.0.1", "8080");
        
        // Call the receiveMessageFromClient function
        receivedValue = recieveMessageFromServer();
        Debug.Log("Received value: " + receivedValue);
        closeConnection();
    }
    
    public void Close()
    {
        closeConnection();
        Debug.Log("Closed");
    }
}
