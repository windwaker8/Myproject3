using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    
    public int Max_HP = 30;
    public int Max_PP = 10;
    public int Atk = 2;
    public int Def = 2;
    public int Luck = 2;
    public int IQ = 2;
    public int Speed = 2;

    public HPDisplay player1_hp;
    public void applyDamage(int dmg)
    {
        if(damage_type = physical)
        {
          Atk - Def;
        }
        if (damage_type = elemental || damage_type = technical)
        {
        Luck - IQ;
        }
    }
}
