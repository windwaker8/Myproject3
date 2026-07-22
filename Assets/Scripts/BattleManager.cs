using UnityEngine;
using System.Collections;

public enum BattleState { PlayerTurn, EnemyTurn, Win, Lose }

public class BattleManager : MonoBehaviour
{   
    public BattleState state;
    public PlayerStats player;
    public EnemyStats enemy;
    public damageCalc calc;
    public DamageSweepBar sweep;
    public List<PlayerStats> party;
    public List <EnemyStats> enemygrp;
    public List<ICombatant> everyone = new List<ICombatant>();


    public Attack enemyPhysical = new Attack {
        moveName = "Bash", type = DamageType.Physical, power = 1,
        atkWeight = 1f, skillWeight = 0f, iqWeight = 0f,
        defendingStat = StatType.Def
    };

    public Attack enemyElemental = new Attack {
        moveName = "BloodSuck", type = DamageType.Elemental, power = 1,
        atkWeight = 1f, skillWeight = 1f, iqWeight = 0f,
        defendingStat = StatType.IQ
    };

    public Attack enemyEvade = new Attack {
        moveName = "Scuttle quickly", type = DamageType.Elemental, power = 0,
        atkWeight = 0f, skillWeight = 0f, iqWeight = 0f,
        defendingStat = StatType.IQ
    };

    // TODO: your turn order list logic goes here

    void Start()
    {
        // TODO: kick off the first turn using your turn order list
    }

public void TurnOrder()
{  
    foreach(var member in party)
    everyone.Add(member);
    foreach(var goon in enemygrp)
    everyone.Add(goon);
}
public void sort(int list arr)
{
    int l = arr.Length;
    for(i =1; i< l; i++ )
    {
        int key = arr[i];
        int j = i-1;
        while(j> 0 && arr[j] > key )
        {
            arr[j+1] = arr[j];
            j = j-1;

        }
        arr[j+1] = key;


    }
}





    public void playerAttacked()
    {
        if (enemy.currentHP <= 0)
        {
            state = BattleState.Win;
            Debug.Log("Bro got mogged.");
            return; 
        }

        // TODO: advance to next combatant in your turn order list
    }

    IEnumerator EnemynoTurn()
    {
        float raw;
        float dmg;
        string damageType;
        yield return new WaitForSeconds(0.5f);
        sweep.hideBar();
        yield return new WaitForSeconds(0.25f);

        // TODO: pick a move from enemy.knownAttacks (or these hardcoded ones) here

        raw = calc.enemyAttack(enemyPhysical);
        dmg = Mathf.Ceil(raw);
        damageType = enemyPhysical.moveName;

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

        // TODO: advance to next combatant in your turn order list
    }
}