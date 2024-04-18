using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeManager : MonoBehaviour
{
    //0 is host; 1 is client
    private int _playerType;
    public int GetPlayerType => _playerType;
    
    public void SetPlayerType(int newType)
    {
        _playerType = newType;
    }
}
