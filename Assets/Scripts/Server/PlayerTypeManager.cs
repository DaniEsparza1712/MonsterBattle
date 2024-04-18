using UnityEngine;

public class PlayerTypeManager : MonoBehaviour
{
    //0 is host; 1 is client
    public int _type;
    public int GetPlayerType => _type;

    public void SetType(int newType)
    {
        _type = newType;
    }
}
