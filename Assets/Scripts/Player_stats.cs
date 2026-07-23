using UnityEngine;
using System.Collections.Generic; // ADDED for List<Attack>

public class PlayerStats : MonoBehaviour, ICombatant
{   

    public PlayerData data;
    public HPDisplay player1_hp;
    public List<Attack> knownAttacks;
    public int level,Max_HP,Max_PP, Atk, Def, Skill, IQ,Speed;
    public int HP_rate, PP_rate,Atk_rate,Def_rate,Skill_rate,IQ_rate,Speed_rate;



    void Awake()
    {   level = data.Level;
        Max_HP = data.baseMaxHP;
        Max_PP = data.baseMaxPP;
        Atk = data.baseAtk;
        Def = data.baseDef;
        Skill = data.baseSkill;
        IQ = data.baseIQ;
        Speed = data.baseSpeed;

        HP_rate = data.hpGrowthRate;
        PP_rate = data.ppGrowthRate;
        Atk_rate = data.atkGrowthRate;
        Def_rate = data.defGrowthRate;
        Skill_rate = data.skillGrowthRate;
        IQ_rate = data.iqGrowthRate;
        Speed_rate = data.speedGrowthRate;

        knownAttacks = new List<Attack>(data.startingAttacks); 


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

    public int statUP(int currentlevel, int growthrate)
    {
        float fgain;

        if (currentlevel > 10 && currentlevel % 4 == 0)
        {
            fgain = (growthrate + 5) / 2;
        }
        else if (currentlevel < 10 && currentlevel % 4 == 0)
        {
            fgain = (growthrate + 3) / 2;
        }
        else
        {
            fgain = (growthrate + Random.Range(0, 4)) / 2;
        }

        int gain = Mathf.RoundToInt(fgain);
        return gain;
    }

    public void levelUP(int rank)
    {
        if (data.exp >= data.requiredEXP)
        {
            rank++;
            Max_HP += statUP(level, HP_rate);
            Max_PP += statUP(level, PP_rate);
            Atk += statUP(level, Atk_rate);
            Def += statUP(level, Def_rate);
            Skill += statUP(level, Skill_rate);
            IQ += statUP(level, IQ_rate);
            Speed += statUP(level, Speed_rate);

            LevelUpData entry = data.levelUpProgression.Find(l => l.level == level);
            if (entry != null && entry.newAttack != null)
            {
                knownAttacks.Add(entry.newAttack);
                Debug.Log($"{GetName()} learned {entry.newAttack.moveName}!");
            }

            Debug.Log($"{GetName()} reached level {rank}! HP:{Max_HP} Atk:{Atk} Def:{Def} Skill:{Skill} IQ:{IQ} Speed:{Speed}");
        }
    }

    public string GetName()
    {
        return data != null ? data.name : gameObject.name;
    }
}
