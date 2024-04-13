using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public TypeManager.Type type;
    [SerializeField]
    private int defense;
    public int GetDefense => defense;
    [SerializeField]
    private int attack;
    public int GetAttack => attack;
    [SerializeField] 
    private int speed;
    public Attack[] attacks = new Attack[4];

    public void AddAttack(int add)
    {
        var top = attack * 2;
        attack = Mathf.Min(attack + Mathf.Abs(add), top);
    }

    public void SubtractAttack(int subtract)
    {
        var bottom = attack / 2;
        attack = Mathf.Max(bottom, attack - Mathf.Abs(subtract));
    }

    public void AddDefense(int add)
    {
        var top = defense * 2;
        defense = Mathf.Min(defense + Mathf.Abs(add), top);
    }

    public void SubtractDefense(int subtract)
    {
        var bottom = defense / 2;
        defense = Mathf.Max(defense - Mathf.Abs(subtract), bottom);
    }
}
