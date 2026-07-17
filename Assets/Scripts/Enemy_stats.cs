using UnityEngine;

public class EnemyStats : MonoBehaviour
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
    //public void ApplyDamage(int dmg)
//{
  //  currentHP = Mathf.Clamp(currentHP - dmg, 0, Max_HP);
   
//}
public void ApplyDamage(int dmg)
{
    currentHP = Mathf.Clamp(currentHP - dmg, 0, Max_HP);
    Debug.Log($"🔴 ENEMY.APPLYDAMAGE CALLED — took {dmg}, currentHP now {currentHP}");
}
}
