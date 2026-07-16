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
    {
        if (obj == ' ' && battleManager.state == BattleState.PlayerTurn)
        {
            float multiplier = sweepBar.GetDamageMultiplier();
            float multDamage = playerAttack(DamageType.Physical, multiplier);
            float damage = Mathf.Ceil(multDamage);
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
    {
        float damage;
        float calcedDamage = (atk - def) * range;

        if (calcedDamage >= 1)
            damage = calcedDamage * 2;
        else
            damage = 1;

        return damage;
    }

    public float playerAttack(DamageType type, float multiplier)
    {
        float adaptAtk = type == DamageType.Physical ? player.Atk : player.Skill;
        float adaptDef = type == DamageType.Physical ? Enemy.Def : Enemy.IQ;
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