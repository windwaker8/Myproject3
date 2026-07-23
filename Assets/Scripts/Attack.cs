using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    public string moveName;
    public DamageType type;
    public int power;
    public bool isBasicAttack = false;

    public float atkWeight;
    public float skillWeight;
    public float iqWeight;

    public StatType defendingStat;

    [Header("Effects")]
    public List<Effect> effects = new List<Effect>();
    public float effectChance = 0.5f;  // chance to apply effects (0-1)
    public bool effectsBeforeDamage = false;
    [Header("Healing")]
    public int healAmount = 0;         // fixed healing amount
    public float healPercent = 0f;     // percentage of damage dealt that heals back (0-1)
}
