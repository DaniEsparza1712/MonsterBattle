using System.Runtime.InteropServices;
using UnityEngine;

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
    void Awake()
    {
        // Call the setupServer function
        setupServer("127.0.0.1", "8080");
    }

    public void SendToClient()
    {
        // Call the sendMessageToClient function
        sendMessageToClient("Hello from C#!");
    }

    public void ReceiveFromClient()
    {
        // Call the receiveMessageFromClient function
        int receivedValue = receiveMessageFromClient();
        Debug.Log("Received value: " + receivedValue);
    }

    public void Close()
    {
        // Call the closeConnection function
        closeConnection();
    }
}
