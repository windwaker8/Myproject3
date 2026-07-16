using UnityEngine;
using System.Collections;

public enum BattleState { PlayerTurn, EnemyTurn, Win, Lose }

public class BattleManager : MonoBehaviour
{   public HPDisplay HP;
    public BattleState state;
    public PlayerStats player;
    public EnemyStats enemy;
    public damageCalc calc;
    public bool fiftyPercent => Random.value < 0.5f;

    void Start()
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

    public void playerAttacked()
    {
        if (enemy.currentHP <= 0)
        {
            state = BattleState.Win;
            Debug.Log("Bro got mogged.");
            return; 
        }

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemynoTurn());
    }

    IEnumerator EnemynoTurn()
    {
        float dmg;
        string damageType;

        if (fiftyPercent)
        {
            dmg = calc.enemyAttack(DamageType.Physical);
            damageType = "Physical";
        }
        else
        {
            dmg = calc.enemyAttack(DamageType.Technical);
            damageType = "Technical";
        }

        player.player1_hp.ApplyDamage(dmg);
        Debug.Log($"The enemy used a {damageType} attack! You took {dmg} damage!");

        if (player.player1_hp.targetHP <= 0)
        {
            state = BattleState.Lose;
            Debug.Log("Generational fumble by you.");
            yield break;
        }

        state = BattleState.PlayerTurn;
    }
}