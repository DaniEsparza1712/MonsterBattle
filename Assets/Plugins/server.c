#include <winsock2.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <time.h>

#define bufferSize 2048

SOCKET serverSocketUDP;
struct sockaddr_in serverAddressUDP, clientAddressUDP;
int addressLengthUDP;

void setupServerUDP(const char *ipAddress, const char *port) {
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        perror("WSAStartup");
        exit(1);
    }

    serverSocketUDP = socket(AF_INET, SOCK_DGRAM, 0);
    if (serverSocketUDP == INVALID_SOCKET) {
        perror("socket");
        exit(1);
    }

    memset(&serverAddressUDP, 0, sizeof(serverAddressUDP));
    serverAddressUDP.sin_family = AF_INET;
    serverAddressUDP.sin_addr.s_addr = inet_addr(ipAddress);
    serverAddressUDP.sin_port = htons(atoi(port));

    if (bind(serverSocketUDP, (struct sockaddr *)&serverAddressUDP, sizeof(serverAddressUDP)) == SOCKET_ERROR) {
        perror("bind");
        exit(1);
    }
}

char* receiveMessageFromClientUDP() {
    char *buffer = malloc(bufferSize);
    if (buffer == NULL) {
        perror("malloc");
        exit(1);
    }

    int n, attempts = 0;
    while (attempts < 5) {
        addressLengthUDP = sizeof(clientAddressUDP);
        n = recvfrom(serverSocketUDP, buffer, bufferSize, 0, (struct sockaddr *)&clientAddressUDP, &addressLengthUDP);
        if (n == SOCKET_ERROR) {
            if (WSAGetLastError() == WSAEWOULDBLOCK) {
                printf("No data available, retrying...\n");
                Sleep(1000);  // Retry after 1 second
                attempts++;
                continue;
            } else {
                perror("recvfrom");
                free(buffer);
                return NULL;
            }
        }
        buffer[n] = '\0';  // Null-terminate the string
        return buffer;
    }
    printf("No messages received after 5 attempts.\n");
    free(buffer);
    return NULL;
}

void closeConnectionUDP() {
    closesocket(serverSocketUDP);
    WSACleanup();
}

//int main() {
//    const char *ipAddress = "127.0.0.1";  // Localhost
//    const char *port = "12345";           // Arbitrary port for the server
//
//    // Initialize server
//    setupServerUDP(ipAddress, port);
//    printf("UDP Server is running on %s:%s\n", ipAddress, port);
//
//    // Try to receive a message
//    char *message = receiveMessageFromClientUDP();
//    if (message != NULL) {
//        printf("Received message: %s\n", message);
//        free(message);
//    }
//
//    // Close server
//    closeConnectionUDP();
//    printf("Server closed.\n");
//
//    return 0;
//}

SOCKET serverSocket, clientSocket;
int addressLength;
struct sockaddr_in serverAddress, clientAddress;

void setupServer(const char *ipAddress, const char *port) {
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        perror("WSAStartup");
        exit(1);
    }

    serverSocket = socket(AF_INET, SOCK_STREAM, 0);
    if (serverSocket == INVALID_SOCKET) {
        perror("socket");
        exit(1);
    }

    serverAddress.sin_family = AF_INET;
    serverAddress.sin_addr.s_addr = inet_addr(ipAddress);
    serverAddress.sin_port = htons(atoi(port));

    if (bind(serverSocket, (struct sockaddr *)&serverAddress, sizeof(serverAddress)) == SOCKET_ERROR) {
        perror("bind");
        exit(1);
    }

    if (listen(serverSocket, 5) == SOCKET_ERROR) {
        perror("listen");
        exit(1);
    }

    addressLength = sizeof(clientAddress);
    clientSocket = accept(serverSocket, (struct sockaddr *)&clientAddress, &addressLength);
    if (clientSocket == INVALID_SOCKET) {
        perror("accept");
        exit(1);
    }
}

void sendMessageToClient(const char *msg) {
    int sent;
    if ((sent = send(clientSocket, msg, strlen(msg), 0)) == SOCKET_ERROR) {
        perror("send");
        exit(1);
    }
}

int receiveMessageFromClient() {
    char buffer[bufferSize];

    int n = recv(clientSocket, buffer, sizeof(buffer), 0);
    if (n == SOCKET_ERROR) {
        perror("recv");
        exit(1);
    }
    buffer[n] = '\0';
    return atoi(buffer);
}

void closeConnection() {
    closesocket(clientSocket);
    closesocket(serverSocket);
    WSACleanup();
}

//int main() {
//    const char *ipAddress = "127.0.0.1";  // Use localhost for testing
//    const char *port = "8080";           // Port number for the server
//
//    // Set up the server on specified IP and port
//    setupServer(ipAddress, port);
//
//    // Wait for and accept a client connection
//    printf("Server is running on %s:%s\n", ipAddress, port);
//    printf("Waiting for client to connect...\n");
//
//    // Send a message to the connected client
//    const char *message = "Hello, client!";
//    sendMessageToClient(message);
//    printf("Message sent to client: %s\n", message);
//
//    // Receive a response from the client
//    int clientResponse = receiveMessageFromClient();
//    printf("Received response from client: %d\n", clientResponse);
//
//    // Close the connection
//    closeConnection();
//    printf("Connection closed.\n");
//
//    return 0;
//}