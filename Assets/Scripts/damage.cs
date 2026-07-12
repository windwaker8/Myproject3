using UnityEngine;
using UnityEngine.InputSystem;

public class damageCalc : MonoBehaviour

{   public PlayerStats player; //connects to Player_stats
    public DamageSweepBar sweepBar; //connects to action_bar
    public HPDisplay HP_meter; //connects to HP_Meter
    public EnemyStats Enemy;
    void OnEnable()
{
    Keyboard.current.onTextInput += GetKeyInput; 
}


private void GetKeyInput(char obj) //checks for space, if pressed scroll the hp meter by a random amount.
{
    if (obj == ' ')
    {
       float multiplier = sweepBar.GetDamageMultiplier();
        float multDamage = DamageCheck(player.Atk, Enemy.Def, multiplier);
        float damage = Mathf.Ceil(multDamage); 
        HP_meter.ApplyDamage(damage);
        sweepBar.hideSlider();
    }
}




void OnDisable()
{
    Keyboard.current.onTextInput -= GetKeyInput;
}
    
 
    private float DamageCheck( float atk, float def, float range){//uses the value returned when space was pressed for damage.
        
        float damage;
        float calcedDamage = (atk - def)* range;

        if (calcedDamage >= 1){
             damage = calcedDamage * 2;
        }
        else if(calcedDamage <= 0)
        {
             damage = 1;
        }

        else
        {
            damage = 1;
        }
        return damage;
       }
   public float playerAttack(DamageType type, float multiplier)
   {
    float adaptAtk = type == DamageType.Physical ? player.Atk : player.Skill; //uses player's atk if damageType is physical, else skill
    float adaptDef = type == DamageType.Physical ? Enemy.Def : Enemy.IQ;    // uses enemy's def if damageType is physical, else IQ

    return DamageCheck(adaptAtk, adaptDef, multiplier);
   }
   public float enemyAttack(DamageType type)
   {
    float adaptAtk = type == DamageType.Physical ? Enemy.Atk : Enemy.Skill;  
    float adaptDef = type == DamageType.Physical ? player.Def : player.IQ;  
    
    float multiplier = Random.Range(0.85f, 1.0f);

    return DamageCheck(adaptAtk, adaptDef, multiplier);
   }

    
}
