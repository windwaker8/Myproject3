using UnityEngine;
using UnityEngine.InputSystem;

public class damageCalc : MonoBehaviour
{
    public PlayerStats player;
    public DamageSweepBar sweepBar;
    public EnemyStats Enemy;
    public BattleManager battleManager;
    public BattleMenu menu;

    void OnEnable()
    {
        Keyboard.current.onTextInput += GetKeyInput;
    }

    private void GetKeyInput(char obj)
    {
        if (obj == ' ' && battleManager.state == BattleState.PlayerTurn)
        {
            float multiplier = sweepBar.GetDamageMultiplier();
            float damage = playerAttack(menu.selectedAttack, multiplier);
            Enemy.ApplyDamage((int)damage);
            sweepBar.PauseSlider();
            battleManager.playerAttacked();
        }
    }

    void OnDisable()
    {
        Keyboard.current.onTextInput -= GetKeyInput;
    }

    private float DamageCheck(ICombatant attacker, ICombatant defender, Attack attack, float range)
    {
        float totalAttack = (attacker.GetStat(StatType.Atk) * attack.atkWeight)
                           + (attacker.GetStat(StatType.Skill) * attack.skillWeight)
                           + (attacker.GetStat(StatType.IQ) * attack.iqWeight);

        float defStat = defender.GetStat(attack.defendingStat);

        float calcedDamage = (totalAttack - defStat + attack.power) * range;

        return calcedDamage >= 1 ? Mathf.Ceil(calcedDamage * 2) : 1;
    }

    public float playerAttack(Attack attack, float multiplier)
    {
        return DamageCheck(player, Enemy, attack, multiplier);
    }

    public float enemyAttack(Attack attack)
    {
        float multiplier = Random.Range(0.85f, 1.0f);
        return DamageCheck(Enemy, player, attack, multiplier); // fixed — enemy attacks, player defends
    }
}