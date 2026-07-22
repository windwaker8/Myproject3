using UnityEngine;
using UnityEngine.EventSystems;

public class BattleMenu : MonoBehaviour
{
    public damageCalc calc;
    public Attack selectedAttack;
    public GameObject attackButton;

    public Attack basicSlash = new Attack {
        moveName = "Slash", type = DamageType.Physical, power = 5,
        atkWeight = 1f, skillWeight = 0f, iqWeight = 0f,
        defendingStat = StatType.Def
    };

    public Attack fireSkill = new Attack {
        moveName = "Fireball", type = DamageType.Elemental, power = 8,
        atkWeight = 0f, skillWeight = 1f, iqWeight = 0f,
        defendingStat = StatType.IQ
    };

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(attackButton);
        selectedAttack = basicSlash;
    }

    public void selectAttack()
    {
        selectedAttack = basicSlash;
        Debug.Log($"{selectedAttack.moveName} selected");
    }

    public void selectSkill()
    {
        selectedAttack = fireSkill;
        Debug.Log($"{selectedAttack.moveName} selected");
    }

    public void SelectGuard() { Debug.Log("Guard not implemented yet"); }
    public void SelectItem() { Debug.Log("Item not implemented yet"); }
    public void SelectRun() { Debug.Log("Run not implemented yet"); }
}