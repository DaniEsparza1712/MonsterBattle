using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Attack")]
public class Attack : ScriptableObject
{
    public enum AnimationType
    {
        Attack,
        Pump
    }
    
    [Header("Attack Info")]
    public string attackName;
    public TypeManager.Type type;
    public AnimationType animation;
    [Header("Attack stats")]
    public int baseDamage;
    public int baseSpeed;
    [Header("Player stats effect")] 
    public int playerDefense;
    public int playerAttack;
    [Header("Enemy stats effect")] 
    public int enemyDefense;
    public int enemyAttack;
    [Header("Cost")]
    public int bpCost;
    [Header("FX")]
    public GameObject attackerFX;
    public GameObject hitFX;
}
