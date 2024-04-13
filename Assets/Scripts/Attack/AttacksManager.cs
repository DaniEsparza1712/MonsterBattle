using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttacksManager
{
    public List<Attack> Attacks;
    public static AttacksManager Instance;
    
    public readonly Dictionary<TypeManager.Type, TypeManager.Type> Weaknesses = new Dictionary<TypeManager.Type, TypeManager.Type>()
    {
        {TypeManager.Type.Psychic, TypeManager.Type.Dark},
        {TypeManager.Type.Dark, TypeManager.Type.Psychic},
        {TypeManager.Type.Air, TypeManager.Type.Rock},
        {TypeManager.Type.Plant, TypeManager.Type.Air},
        {TypeManager.Type.Rock, TypeManager.Type.Plant}
    };

    public AttacksManager(List<Attack> attackList)
    {
        Attacks = attackList;
    }

    public int EvalDamage(Attack attack, Monster attacker, Monster attacked)
    {
        float typeMultiplier = 1.0f;

        if (Weaknesses.ContainsKey(attacked.type))
        {
            if (Weaknesses[attacked.type] == attack.type)
                typeMultiplier = 2.0f;
        }
        if (attacked.type == attack.type)
            typeMultiplier = 0.5f;
        
        var statsMultiplier = (float)attacker.GetAttack / attacked.GetDefense;
        var damage = (((2 * attack.baseDamage * statsMultiplier) / 40.0f) + 2) * typeMultiplier;
        
        Debug.Log($"Damage: {Mathf.RoundToInt(damage)}");
        
        return Mathf.RoundToInt(damage);
    }
}
