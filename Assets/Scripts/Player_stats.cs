using UnityEngine;

public enum DamageType { Physical, Elemental, Technical }

public class PlayerStats : MonoBehaviour
{
    public int Max_HP = 30;
    public int Max_PP = 10;
    public int Atk = 2;
    public int Def = 2;
    public int Skill = 2;
    public int IQ = 2;
    public int Speed = 2;

    public HPDisplay player1_hp;

    
    

    public int DamageFormula(int playerAtk, int enemyDef)
    {    int rawDamage;
        return rawDamage = 2 * (playerAtk - enemyDef);
    }
}