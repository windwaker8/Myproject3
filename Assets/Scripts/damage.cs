using UnityEngine;
using UnityEngine.InputSystem;

public class damageCalc : MonoBehaviour

{   public float Atk = 10;
    public float Def = 1;
    public DamageSweepBar sweepBar; //connects to action_bar
    public HPDisplay HP_meter; //connects to HP_Meter
    
    void OnEnable()
{
    Keyboard.current.onTextInput += GetKeyInput; 
}


private void GetKeyInput(char obj) //checks for space, if pressed scroll the hp meter by a random amount.
{
    if (obj == ' ')
    {
       float multiplier = sweepBar.GetDamageMultiplier();
        float rawDamage = calcDamage(Atk, Def, multiplier);
        float damage = Mathf.Ceil(rawDamage); 
        HP_meter.ApplyDamage(damage);
       
    }
}




void OnDisable()
{
    Keyboard.current.onTextInput -= GetKeyInput;
}
    
 
    float calcDamage( float atk, float def, float range){//uses the value returned when space was pressed for damage.
       

        float damage = (atk - def)* range;
        return damage;
       }
   
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
