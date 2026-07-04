using UnityEngine;

public class RollingDigit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] frames;

    public int framesPerDigit = 12; //12 frames of animation for the numbers 
    public int blankDigitIndex = 10; //blank digits also in the sprites, index is 10.
    private int totalDigitSlots = 11; //0 -9 take up 10 slots total, the blanks take up the last slot.
    private int totalFrames;

    public int currentDigit = 0;   // -1 = blank sprites
    public int targetDigit = 0;    //same as the line above

    private float currentFramePos; 
    private float targetFramePos;
    private bool isRolling = false;
    private float framesPerSecond;  // set by HPDisplay based on tick duration

    void Awake()
    {
        totalFrames = totalDigitSlots * framesPerDigit;  // 12 * 11 for a total of 132 frames.


       //throws an error if there are no frames
        if (frames == null || frames.Length != totalFrames)
            Debug.LogError($"{name}: expects {totalFrames} frames, found {(frames == null ? 0 : frames.Length)}");

        SnapToCurrent();
    }

    // Called by HPDisplay once on Start to set animation speed
    public void SetTickDuration(float tickDuration)
    {
        // Must scrub through all 12 sub-frames within one tick duration
        framesPerSecond = framesPerDigit / tickDuration;
    }

    // Instantly snap to currentDigit with no animation, for the start
    public void SnapToCurrent()
    {
        int slot = currentDigit >= 0 ? currentDigit : blankDigitIndex;
        currentFramePos = slot * framesPerDigit;
        targetFramePos = currentFramePos;
        targetDigit = currentDigit;
        isRolling = false;
        ApplySprite();
    }

    // Called by HPDisplay each tick when this digit's value changes, rolling back takes the shortest path (0-9)
 public void RollToDigit(int newDigit)
{
    targetDigit = newDigit;
    int targetSlot = newDigit >= 0 ? newDigit : blankDigitIndex;
    int currentSlot = currentDigit >= 0 ? currentDigit : blankDigitIndex;

    // Blank transitions: snap instantly, no roll animation
    if (newDigit < 0 || currentDigit < 0)
    {
        currentFramePos = targetSlot * framesPerDigit;
        targetFramePos = currentFramePos;
        currentDigit = newDigit;
        isRolling = false;
        ApplySprite();
        return;
    }
    // compares the difference between 2 numbers. Eg: 0 and 9, takes the smaller difference by +/- 10
    
    int diff = targetSlot - currentSlot;
    if (diff > 5) diff -= 10;
    else if (diff < -5) diff += 10;

    targetFramePos = currentFramePos + diff * framesPerDigit;
    isRolling = diff != 0;
}

    public bool IsRolling() => isRolling;

    void Update()
    {
        if (!isRolling) return;
       
        float direction = Mathf.Sign(targetFramePos - currentFramePos);
        //calculates the direction (duh). 9-0 is 1, 0-9 is -1
        
        
        currentFramePos += direction * framesPerSecond * Time.deltaTime; 
        //rolls in the direction at framesPerSecond 

       bool reached; //direction will only be 0 if the frame has been reached
       reached = direction > 0 ? currentFramePos >= targetFramePos 
     : currentFramePos <= targetFramePos;
        if (reached)

{   //if targetDigit is bigger than or equal to 0, use target digit, otherwise uses the blank digits
    int targetSlot = targetDigit >= 0 ? targetDigit : blankDigitIndex;
    currentFramePos = targetSlot * framesPerDigit;  
    
    
    //snaps to the correct target once roll is done to avoid glitches 
    currentDigit = targetDigit;
    isRolling = false;
}

        ApplySprite();
    }

    private void ApplySprite()
{
    float digitRange = 10 * framesPerDigit; // the circular 0-9 range only
    float pos = currentFramePos;

    // Wraps for digit positions
    if (targetDigit >= 0 && currentDigit >= 0)
    {
        if (pos < 0) pos += digitRange;
        else if (pos >= digitRange) pos -= digitRange;
    }
    
    //displays the sprite at its position using its index, clamped within the totalFrames.
    int index = Mathf.Clamp(Mathf.RoundToInt(pos), 0, totalFrames - 1);

//checks if everything was set up porperly, otherwise wont run.
    if (frames != null && frames.Length > index && frames[index] != null)
        spriteRenderer.sprite = frames[index];
}
}