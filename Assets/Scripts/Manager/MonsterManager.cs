using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //0 is host; 1 is client
    public int _player;
    
    [Header("Player 1")]
    public PlayerManager player1;
    public GameObject[] player1Monsters = new GameObject[3];
    
    [Header("Player 2")]
    public PlayerManager player2;
    public GameObject[] player2Monsters = new GameObject[3];
    
    // Awake is called before start
    void Awake()
    {
        _player = GameObject.Find("TypeManager").GetComponent<PlayerTypeManager>().GetPlayerType;

        if (_player == 0)
        {
            player1.SetMonsters(player1Monsters);
            player2.SetMonsters(player2Monsters);
        }
        else
        {
            player1.SetMonsters(player2Monsters);
            player2.SetMonsters(player1Monsters);
        }
        Debug.Log(_player);
        player1.InitializeMonster();
        player2.InitializeMonster();
        player1.gameObject.GetComponent<PlayerUIManager>().InitializeUI();
        player2.gameObject.GetComponent<PlayerUIManager>().InitializeUI();
    }
}
