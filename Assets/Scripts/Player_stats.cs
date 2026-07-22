using UnityEngine;
using System.Collections.Generic; // ADDED for List<Attack>

public class PlayerStats : MonoBehaviour, ICombatant
{   

    public int Max_HP = 30;
    public int Max_PP = 10;
    public int Atk = 2;
    public int Def = 2;
    public int Skill = 2;
    public int IQ = 2;
    public int Speed = 2;

    public HPDisplay player1_hp;
    public List<Attack> knownAttacks;



    void Awake()
    {
        player1_hp.startingHP = Max_HP;
        player1_hp.targetHP = Max_HP;
    }

    public int GetStat(StatType type)
{
    switch (type)
    {
        case StatType.Atk: return Atk;
        case StatType.Def: return Def;
        case StatType.Skill: return Skill;
        case StatType.IQ: return IQ;
        case StatType.Speed: return Speed;
        default: return 0;
    }
}
}