using UnityEngine;
using System.Collections;

public class HPDisplay : MonoBehaviour
{
    public int startingHP = 300;
    public int targetHP = 300;
    public float tickDuration = 0.16f;  // seconds per HP step
    public bool isGuard;

    private bool lastIsGuard;
    private int displayedHP;            // the HP value currently being shown/animating
    private float tickTimer;
    private bool initialized = false;

    public RollingDigit hundreds; // references its children 
    public RollingDigit tens;
    public RollingDigit ones;

    void Start()
    {
        StartCoroutine(InitAfterAwake());
    }

    IEnumerator InitAfterAwake()
    {
        yield return null; // wait one frame for all RollingDigit Awake() calls to finish
        
        //if isGuard is active, double the tick duration. Simulates guarding
        lastIsGuard = isGuard;
       float effectiveTickDuration = isGuard ? tickDuration * 2f : tickDuration; 
       hundreds.SetTickDuration(effectiveTickDuration);
       tens.SetTickDuration(effectiveTickDuration);
       ones.SetTickDuration(effectiveTickDuration);

        // Snap display instantly to startingHP
        displayedHP = startingHP;
        int hp = Mathf.Max(startingHP, 0);
        bool showHundreds = hp >= 100;
        bool showTens = hp >= 10 || showHundreds;

        hundreds.currentDigit = showHundreds ? (hp / 100) % 10 : -1; 
        //-1 is blank. so damage is 30 and not 030
        tens.currentDigit = showTens ? (hp / 10) % 10 : -1;
        ones.currentDigit = hp % 10;

        hundreds.SnapToCurrent();  //calls Rolling_Digit's function
        tens.SnapToCurrent();
        ones.SnapToCurrent();

        tickTimer = effectiveTickDuration;
        initialized = true;
    }

    public void ApplyDamage(float damage)
{
    int dmg = isGuard? Mathf.RoundToInt(damage)/2 : Mathf.RoundToInt(damage); //halves damage if isGuard is true.
    targetHP = Mathf.Max(0, targetHP - dmg);
     Debug.Log("Dealt"+" "+ dmg +" "+ "damage");
}

    void Update()
    {
        if (!initialized) return;
        if (displayedHP == targetHP) return;

        
        if (isGuard != lastIsGuard)
{
    lastIsGuard = isGuard; //Allows for guard to be triggered when pausing the playspace to slow down the speed
    //does not affect damage unless the hp meter was damaged again
    float newDuration = isGuard ? tickDuration * 2f : tickDuration;
    hundreds.SetTickDuration(newDuration);
    tens.SetTickDuration(newDuration);
    ones.SetTickDuration(newDuration);
}

        tickTimer -= Time.deltaTime;
        if (tickTimer > 0f) return;

               // Only tick if no digit is still mid-animation from the last tick,
        // so ticks don't overlap or get skipped (called from rolling_digit again)
        if (hundreds.IsRolling() || tens.IsRolling() || ones.IsRolling()) return;


        // Reset the timer for the next tick, using the current guard-adjusted duration
        tickTimer = isGuard ? tickDuration * 2f : tickDuration;

        // Step displayedHP one tick toward targetHP
        displayedHP += displayedHP < targetHP ? 1 : -1;

        // Recompute which digits should be visible/blank at the new displayedHP
        int hp = Mathf.Max(displayedHP, 0);
        bool showHundreds = hp >= 100;
        bool showTens = hp >= 10 || showHundreds;

        int newOnes = hp % 10;
        int newTens = showTens ? (hp / 10) % 10 : -1;
        int newHundreds = showHundreds ? (hp / 100) % 10 : -1;

        // Only trigger a roll animation on digits whose value actually changed this tick
        if (newOnes != ones.currentDigit)
            ones.RollToDigit(newOnes);
        if (newTens != tens.currentDigit)
            tens.RollToDigit(newTens);
        if (newHundreds != hundreds.currentDigit)
            hundreds.RollToDigit(newHundreds);
    }
}