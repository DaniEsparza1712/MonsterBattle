using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeManager
{
    public enum Type
    {
        Normal,
        Psychic,
        Dark,
        Air,
        Plant,
        Rock
    }

    public readonly Dictionary<Type, Type> Weaknesses = new Dictionary<Type, Type>()
    {
        {Type.Psychic, Type.Dark},
        {Type.Dark, Type.Psychic},
        {Type.Air, Type.Rock},
        {Type.Plant, Type.Air},
        {Type.Rock, Type.Plant}
    };
}
