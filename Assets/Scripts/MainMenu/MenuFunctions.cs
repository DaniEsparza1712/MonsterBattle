using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//95 is for start battle on server
public class MenuFunctions : MonoBehaviour
{
    private string _joinCode;
    public string GetJoinCode => _joinCode;

    public TMP_Text joinCodeTMP;
    public TMP_InputField joinCodeInput;
    public Server server;
    public Client client;
    
    public void GenerateJoinCode()
    {
        _joinCode = Random.Range(1000, 1500).ToString();
        joinCodeTMP.text = $"Join Code: {_joinCode}";
    }

    public void GetJoinCodeFromInput()
    {
        _joinCode = joinCodeInput.text;
        Debug.Log(_joinCode);
    }

    public void SendJoinCode()
    {
        StartCoroutine(client.SendMessageToServer(GetJoinCode));
    }

    public void ListenForJoinCode()
    {
        StartCoroutine(server.ExpectMessageFromServer(int.Parse(GetJoinCode)));
    }

    public void SendBattle()
    {
        StartCoroutine(server.SendThread(95));
    }
    
    public void ListenForBattle()
    {
        StartCoroutine(client.ReceiveThread());
    }
}
