#include <winsock2.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <time.h>

#define bufferSize 2048

SOCKET clientSocketUDP;
struct sockaddr_in serverAddressUDP;
int addressLengthUDP;

SOCKET clientSocketClient;
struct sockaddr_in serverAddressClient;

void setupClient(const char *ipAddress, const char *port) {
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        perror("WSAStartup");
        exit(1);
    }

    clientSocketClient = socket(AF_INET, SOCK_STREAM, 0);
    if (clientSocketClient == INVALID_SOCKET) {
        perror("socket");
        exit(1);
    }

    serverAddressClient.sin_family = AF_INET;
    serverAddressClient.sin_addr.s_addr = inet_addr(ipAddress);
    serverAddressClient.sin_port = htons(atoi(port));

    int maxRetries = 5;  // Maximum number of connection retries
    int retries = 0;
    while (retries < maxRetries) {
        if (connect(clientSocketClient, (struct sockaddr *)&serverAddressClient, sizeof(serverAddressClient)) == SOCKET_ERROR) {
            if (WSAGetLastError() == WSAETIMEDOUT) {
                fprintf(stderr, "Connection attempt timed out. Retrying...\n");
            } else {
                perror("connect");
            }
            retries++;
            Sleep(1000);  // Wait for 1 second before retrying
        } else {
            return;
        }
    }
}


void sendMessageToServer(const char *msg) {
    int sent;
    if ((sent = send(clientSocketClient, msg, strlen(msg), 0)) == SOCKET_ERROR) {
        perror("send");
        exit(1);
    }
}

int receiveMessageFromServer() {
    char buffer[bufferSize];

    int n = recv(clientSocketClient, buffer, sizeof(buffer), 0);
    if (n == SOCKET_ERROR) {
        perror("recv");
        exit(1);
    }
    buffer[n] = '\0';
    return atoi(buffer);
}

void closeConnection() {
    closesocket(clientSocketClient);
    WSACleanup();
}







void setupClientUDP(const char *ipAddress, const char *port) {
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        perror("WSAStartup");
        exit(1);
    }

    clientSocketUDP = socket(AF_INET, SOCK_DGRAM, 0);
    if (clientSocketUDP == INVALID_SOCKET) {
        perror("socket");
        exit(1);
    }

    memset(&serverAddressUDP, 0, sizeof(serverAddressUDP));
    serverAddressUDP.sin_family = AF_INET;
    serverAddressUDP.sin_addr.s_addr = inet_addr(ipAddress);
    serverAddressUDP.sin_port = htons(atoi(port));
}

void sendMessageToServerUDP(const char *msg) {
    int attempts = 0;
    int sentBytes;
    while (attempts < 5) {
        sentBytes = sendto(clientSocketUDP, msg, strlen(msg), 0, (struct sockaddr *)&serverAddressUDP, sizeof(serverAddressUDP));
        if (sentBytes == SOCKET_ERROR) {
            perror("sendto");
            Sleep(1000); // Wait for 1 second before retrying
            attempts++;
        } else {
            printf("Message sent: %s\n", msg);
            break;
        }
    }
}

int receiveMessageFromServerUDP() {
    char buffer[bufferSize];
    int n;

    addressLengthUDP = sizeof(serverAddressUDP);
    n = recvfrom(clientSocketUDP, buffer, bufferSize, 0, (struct sockaddr *)&serverAddressUDP, &addressLengthUDP);
    if (n == SOCKET_ERROR) {
        perror("recvfrom");
        return -1;
    }
    buffer[n] = '\0';  // Null-terminate the string
    printf("Received message from server: %s\n", buffer);
    return atoi(buffer); // Assuming the server sends back an integer
}

void closeConnectionUDP() {
    closesocket(clientSocketUDP);
    WSACleanup();
}

int main() {
    const char *serverIP = "127.0.0.1";  // Server IP address
    const char *serverPort = "12345";    // Server port

    // Set up the client
    setupClientUDP(serverIP, serverPort);

    // Send a message to the server
    const char *message = "Hello from client";
    sendMessageToServerUDP(message);

    // Optionally receive a response from the server
    if (receiveMessageFromServerUDP() == -1) {
        printf("Failed to receive a valid response from server.\n");
    }

    // Close the client socket
    closeConnectionUDP();
    printf("Client connection closed.\n");

    return 0;
}
