using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, ICombatant
{
    [Header("Enemy Data")]
    public EnemyData enemyData;

    [Header("Runtime State")]
    public int currentHP;
    public List<Attack> knownAttacks = new List<Attack>();

    void Awake()
    {
        if (enemyData != null)
        {
            currentHP = enemyData.maxHP;
            knownAttacks = new List<Attack>(enemyData.attacks);
        }
        else
        {
            currentHP = 30;
            knownAttacks = new List<Attack>();
        }
    }

    public int GetStat(StatType type)
    {
        if (enemyData == null)
            return 0;

        switch (type)
        {
            case StatType.Atk: return enemyData.atk;
            case StatType.Def: return enemyData.def;
            case StatType.Skill: return enemyData.skill;
            case StatType.IQ: return enemyData.iq;
            case StatType.Speed: return enemyData.speed;
            default: return 0;
        }
    }

    public string GetName()
    {
        return enemyData != null ? enemyData.name : gameObject.name;
    }

    public void ApplyDamage(int dmg)
    {
        if (enemyData != null)
            currentHP = Mathf.Clamp(currentHP - dmg, 0, enemyData.maxHP);
        else
            currentHP = Mathf.Clamp(currentHP - dmg, 0, 30);

        Debug.Log($"🔴 ENEMY.APPLYDAMAGE CALLED — took {dmg}, currentHP now {currentHP}");
    }
}

