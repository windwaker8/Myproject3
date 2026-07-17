using UnityEngine;
using UnityEngine.InputSystem;

public class damageCalc : MonoBehaviour
{
    public PlayerStats player;
    public DamageSweepBar sweepBar;
    public EnemyStats Enemy;
    public BattleManager battleManager;

    void OnEnable()
    {
        Keyboard.current.onTextInput += GetKeyInput;
        
    }

    private void GetKeyInput(char obj)
    {   Debug.Log($"Key {obj} received.");
        if (obj == ' ' && battleManager.state == BattleState.PlayerTurn)
        {   Debug.Log("Pow!");
            float multiplier = sweepBar.GetDamageMultiplier();
            float damage= playerAttack(DamageType.Physical, multiplier);
            Enemy.ApplyDamage((int)damage);
            sweepBar.hideSlider();

            battleManager.playerAttacked();
        }
    }

    void OnDisable()
    {
        Keyboard.current.onTextInput -= GetKeyInput;
        
    }

    private float DamageCheck(float atk, float def, float range)
    {   float raw;
        float damage;
        float calcedDamage = (atk - def) * range;

        if (calcedDamage >= 1)
           { raw = calcedDamage * 2;
            damage = Mathf.Ceil(raw);
           }
        else
        {
            damage = 1;
        }
        return damage;
    }

    public float playerAttack(DamageType type, float multiplier)
{
     float adaptAtk = type == DamageType.Physical ? player.Atk : player.Skill;  //Checks if physical, uses player.atk, else uses skill
     float adaptDef = type == DamageType.Physical ? Enemy.Def : Enemy.IQ; // Checks if physical, uses enemy.def, else use iq.
     return DamageCheck(adaptAtk, adaptDef, multiplier);
}

    public float enemyAttack(DamageType type)
    {
        float adaptAtk = type == DamageType.Physical ? Enemy.Atk : Enemy.Skill; //Same logic, just reversed to use enemy atk/skill
        float adaptDef = type == DamageType.Physical ? player.Def : player.IQ;  
        float multiplier = Random.Range(0.85f, 1.0f);
        return DamageCheck(adaptAtk, adaptDef, multiplier);
    }
}