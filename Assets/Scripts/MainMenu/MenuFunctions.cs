using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        Debug.Log("d");
        client.SetPortPref(8080);
        StartCoroutine(client.SendMessageToServer(GetJoinCode));
    }

    public void ListenForJoinCode()
    {
        server.SetPortPref(8080);
        StartCoroutine(server.ExpectMessageFromServer(int.Parse(GetJoinCode)));
    }

    public void setServerPlayerType()
    {
        PlayerPrefs.SetInt("Type", 0);
    }

    public void setClientPlayerType()
    {
        PlayerPrefs.SetInt("Type", 1);
    }
}
