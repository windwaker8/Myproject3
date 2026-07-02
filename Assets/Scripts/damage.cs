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
        bool inGreen = sweepBar.IsInGreenZone();
        float damage = calcDamage(Atk, Def, inGreen);
        HP_meter.ApplyDamage(damage);
    }
}

float calcDamage(float atk, float def, bool isGreenZone)
{
    float damage = atk - def;
    if (!isGreenZone)
        damage *= Random.Range(0.5f, 0.75f);
    return Mathf.Max(damage, 0f);
}
void OnDisable()
{
    Keyboard.current.onTextInput -= GetKeyInput;
}
    
 
    float calcDamage( float atk, float def){
       

        float damage = atk - def;
        return damage;
       }
   
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
