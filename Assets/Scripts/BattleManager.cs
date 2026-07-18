using UnityEngine;
using System.Collections;

public enum BattleState { PlayerTurn, EnemyTurn, Win, Lose }

public class BattleManager : MonoBehaviour
{   
    public BattleState state;
    public PlayerStats player;
    public EnemyStats enemy;
    public damageCalc calc;
    public bool fiftyPercent => Random.value < 0.5f;
    public DamageSweepBar sweep;

    private bool Pending;

    void Start()
    {
        Speedcheck();
        Pending = true;
        if(state == BattleState.EnemyTurn)
        StartCoroutine(EnemynoTurn());
    }

    public void playerAttacked()
    {
        if (enemy.currentHP <= 0)
        {
            state = BattleState.Win;
            Debug.Log("Bro got mogged.");
            return; 
        }
       Speedcheck();
       if(Pending)
       {
        Pending = false;
        state = BattleState.EnemyTurn;
       }
       else
        {   Pending = true;
            Speedcheck();
            if(state == BattleState.EnemyTurn)
            StartCoroutine(EnemynoTurn());
        }
    }
    private void Speedcheck()
    {
        if (player.Speed > enemy.Speed)
        {
            state = BattleState.PlayerTurn;
        }
        else if (enemy.Speed > player.Speed)
        {
            state = BattleState.EnemyTurn;
        }
        else
        {
            state = fiftyPercent ? BattleState.PlayerTurn : BattleState.EnemyTurn;
        }
    }
    IEnumerator EnemynoTurn()
    {   float raw;
        float dmg;
        string damageType;
        yield return new WaitForSeconds(0.5f);
        sweep.hideBar();
        yield return new WaitForSeconds(0.25f);

        

        if (fiftyPercent)
        {
            raw = calc.enemyAttack(DamageType.Physical);
            dmg = Mathf.Ceil(raw);
            damageType = "Physical";
        }
        else
        {
            raw = calc.enemyAttack(DamageType.Elemental);
            dmg = Mathf.Ceil(raw);
            damageType = "Elemental";
        }

        player.player1_hp.ApplyDamage(dmg);
        Debug.Log($"The enemy used a {damageType} attack! You took {dmg} damage!");
       
        sweep.showBar();
        sweep.ResetSlider();

        if (player.player1_hp.targetHP <= 0)
        {
            state = BattleState.Lose;
            Debug.Log("Generational fumble by you.");
            yield break;
        }
         if(Pending)
       {
        Pending = false;
        state = BattleState.PlayerTurn;
       }
       else
        {   Pending = true;
            Speedcheck();
            if(state == BattleState.EnemyTurn)
            StartCoroutine(EnemynoTurn());

            else
            {
                sweep.showBar();
                sweep.ResetSlider();
            }
            
        }

        
    }
}