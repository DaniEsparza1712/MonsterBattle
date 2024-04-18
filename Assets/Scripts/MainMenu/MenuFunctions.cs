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
<<<<<<< Updated upstream
        server.SetPortPref(8081);
        StartCoroutine(server.SendMessageToServer(GetJoinCode));
=======
        StartCoroutine(client.SendMessageToServer(GetJoinCode));
>>>>>>> Stashed changes
    }

    public void ListenForJoinCode()
    {
        StartCoroutine(server.ExpectMessageFromServer(int.Parse(GetJoinCode)));
    }
}
