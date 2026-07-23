using UnityEngine;

public enum StatusEffect { Poison, Paralysis, Burn, Freeze, Sleep, Confusion }

[System.Serializable]
public class Effect
{
    public StatusEffect type;
    public StatType affectedStat;      // which stat this affects (if any)
    public int statModifierAmount;     // how much to modify the stat
    public int duration;               // turns the effect lasts
}

