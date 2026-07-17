using UnityEngine;

public enum DamageType { Physical, Elemental, Technical }

public class PlayerStats : MonoBehaviour
{
    public int Max_HP = 30;
    public int currentHP;
    public int Max_PP = 10;
    public int Atk = 5;
    public int Def = 3;
    public int Skill = 5;
    public int IQ = 3;
    public int Speed = 2;

    public HPDisplay player1_hp;

    
    
void Awake(){
    player1_hp.startingHP = Max_HP;
    player1_hp.targetHP = Max_HP;
}
   
  
}
