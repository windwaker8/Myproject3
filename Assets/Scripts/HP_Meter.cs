using UnityEngine;

public class HPDisplay : MonoBehaviour
{
    public int startingHP = 300;   // HP shown immediately on scene start, no roll animation
    public int targetHP = 300;     // HP value the meter should animate (roll) towards
    private int lastSetHP;         // tracks last value pushed to the digits, so we only push on change

    public RollingDigit hundreds;
    public RollingDigit tens;
    public RollingDigit ones;

    void Start()
    {
        // Snap digits instantly to startingHP -- no animated roll on scene load.
        int hp = Mathf.Max(startingHP, 0);
        bool showHundreds = hp >= 100;
        bool showTens = hp >= 10 || showHundreds;

        hundreds.currentDigit = showHundreds ? (hp / 100) % 10 : -1;
        tens.currentDigit = showTens ? (hp / 10) % 10 : -1;
        ones.currentDigit = hp % 10;

        hundreds.SnapToCurrent();
        tens.SnapToCurrent();
        ones.SnapToCurrent();

        lastSetHP = startingHP;
    }

    void Update()
    {
        if (targetHP != lastSetHP)
        {
            SetDigits(targetHP);
            lastSetHP = targetHP;
        }
    }

    private void SetDigits(int hp)
    {
        hp = Mathf.Max(hp, 0); // guard against negative HP

        bool showHundreds = hp >= 100;
        bool showTens = hp >= 10 || showHundreds;

        ones.targetDigit = hp % 10;
        tens.targetDigit = showTens ? (hp / 10) % 10 : -1;
        hundreds.targetDigit = showHundreds ? (hp / 100) % 10 : -1;
    }
}