using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersManager : MonoBehaviour
{
    private int _playerType;
    
    [Header("Player 1")] 
    public PlayerManager player1Manager;
    public PlayerUIManager player1UIManager;
    public GameObject[] player1Monsters = new GameObject[3];
    
    [Header("Player 2")]
    public PlayerManager player2Manager;
    public PlayerUIManager player2UIManager;
    public GameObject[] player2Monsters = new GameObject[3];

    private void Awake()
    {
        _playerType = GameObject.Find("PlayerTypeManager").GetComponent<PlayerTypeManager>().GetPlayerType;

        if (_playerType == 0)
        {
            player1Manager.SetMonsters(player1Monsters);
            player2Manager.SetMonsters(player2Monsters);
        }
        else
        {
            player1Manager.SetMonsters(player2Monsters);
            player2Manager.SetMonsters(player1Monsters);
        }
        
        player1Manager.InitializePlayerManager();
        player2Manager.InitializePlayerManager();
        player1UIManager.InitializeUI();
        player2UIManager.InitializeUI();
    }
}
