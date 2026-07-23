using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Combat/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Base Stats")]
    public int Level = 1;
    public int baseMaxHP = 30;
    public int baseMaxPP = 10;
    public int baseAtk = 2;
    public int baseDef = 2;
    public int baseSkill = 2;
    public int baseIQ = 2;
    public int baseSpeed = 2;
    public int exp = 0;
    public int requiredEXP = 10;

    [Header("Growth Rates")]
    public int hpGrowthRate = 5;
    public int ppGrowthRate = 3;
    public int atkGrowthRate = 5;
    public int defGrowthRate = 3;
    public int skillGrowthRate = 3;
    public int iqGrowthRate = 4;
    public int speedGrowthRate = 2;



    [Header("Starting Moves")]
    public List<Attack> startingAttacks = new List<Attack>();

    [Header("Level Up Progression")]
    public List<LevelUpData> levelUpProgression = new List<LevelUpData>();
}
