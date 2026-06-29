using UnityEngine;

public class HPDisplay : MonoBehaviour
{
    public int targetHP = 300;     // actual HP
    public int displayedHP = 300;  // rolling HP shown
    public float tickDelay = 1f / 30f; // one HP per 1/30 sec (~30 HP/sec)

    public RollingDigit hundreds;
    public RollingDigit tens;
    public RollingDigit ones;

    private float timer;

    void Update()
    {
         // Start with 300 HP displayed
    displayedHP = 300;

    // Force a drop to 123 HP so the roll animates
    targetHP = 123;
        timer -= Time.deltaTime;

        if (displayedHP != targetHP && timer <= 0f)
        {
            if (displayedHP > targetHP) displayedHP--;
            else if (displayedHP < targetHP) displayedHP++;

            timer = tickDelay;
        }

        int h = (displayedHP / 100) % 10;
        int t = (displayedHP / 10) % 10;
        int o = displayedHP % 10;

        hundreds.targetDigit = h;
        tens.targetDigit = t;
        ones.targetDigit = o;
    }
}
