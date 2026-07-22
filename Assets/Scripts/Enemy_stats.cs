using UnityEngine;

public class EnemyStats : MonoBehaviour, ICombatant
{  


    public int Max_HP = 30;
    public int currentHP;
    public int Max_PP = 10;
    public int Atk = 7;
    public int Def = 3;
    public int Skill = 5;
    public int IQ = 2;
    public int Speed = 255;




    // Update is called once per frame
    void Awake(){
        currentHP = Max_HP;
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


public void ApplyDamage(int dmg)
{
    currentHP = Mathf.Clamp(currentHP - dmg, 0, Max_HP);
    Debug.Log($"🔴 ENEMY.APPLYDAMAGE CALLED — took {dmg}, currentHP now {currentHP}");
}
}
