using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Paper", menuName = "Paper")]
public class Paper : ScriptableObject
{
    
    public string headline;
    public string story;
    public float effectValue;
    public EffectTypes effectType;


}

public enum EffectTypes
{
    woolValue,
    eggValue,
    milkValue,

    sheepValue,
    chickenValue,
    cowValue,

    sheepHealth,
    chickenHealth,
    cowHealth,

    sheepBreeding,
    chickenBreeding,
    cowBreeding,

    woolProduction,
    eggProduction,
    milkProduction,
}
