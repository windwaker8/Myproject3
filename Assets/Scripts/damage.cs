using UnityEngine;
using UnityEngine.InputSystem;

public class damageCalc : MonoBehaviour

{   public float Atk = 10;
    public float Def = 1;
    public DamageSweepBar sweepBar;
    public HPDisplay HP_meter;
    
    void OnEnable()
{
    Keyboard.current.onTextInput += GetKeyInput;
}


private void GetKeyInput(char obj)
{
    if (obj == ' ')
    {
       float multiplier = sweepBar.GetDamageMultiplier();
        float rawDamage = calcDamage(Atk, Def, multiplier);
        float damage = Mathf.Ceil(rawDamage); 
        HP_meter.ApplyDamage(damage);
        Debug.Log("Dealt"+" "+ damage +" "+ "damage");
    }
}




void OnDisable()
{
    Keyboard.current.onTextInput -= GetKeyInput;
}
    
 
    float calcDamage( float atk, float def, float range){
       

        float damage = (atk - def)* range;
        return damage;
       }
   
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
