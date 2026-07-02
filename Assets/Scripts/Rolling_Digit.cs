using UnityEngine;

public class RollingDigit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] frames;

    public int framesPerDigit = 12;
    public int blankDigitIndex = 10;
    private int totalDigitSlots = 11;
    private int totalFrames;

    public int currentDigit = 0;   // -1 = blank
    public int targetDigit = 0;    // -1 = blank

    private float currentFramePos;
    private float targetFramePos;
    private bool isRolling = false;
    private float framesPerSecond;  // set by HPDisplay based on tick duration

    void Awake()
    {
        totalFrames = totalDigitSlots * framesPerDigit;

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

    // Instantly snap to currentDigit with no animation
    public void SnapToCurrent()
    {
        int slot = currentDigit >= 0 ? currentDigit : blankDigitIndex;
        currentFramePos = slot * framesPerDigit;
        targetFramePos = currentFramePos;
        targetDigit = currentDigit;
        isRolling = false;
        ApplySprite();
    }

    // Called by HPDisplay each tick when this digit's value changes
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
        currentFramePos += direction * framesPerSecond * Time.deltaTime;

        bool reached = direction > 0 ? currentFramePos >= targetFramePos
                                     : currentFramePos <= targetFramePos;
        if (reached)
{
    int targetSlot = targetDigit >= 0 ? targetDigit : blankDigitIndex;
    currentFramePos = targetSlot * framesPerDigit;  // snap to canonical frame, not drifted value
    currentDigit = targetDigit;
    isRolling = false;
}

        ApplySprite();
    }

    private void ApplySprite()
{
    float digitRange = 10 * framesPerDigit; // the circular 0-9 range only
    float pos = currentFramePos;

    // Only wrap for actual digit positions, not the blank slot
    if (targetDigit >= 0 && currentDigit >= 0)
    {
        if (pos < 0) pos += digitRange;
        else if (pos >= digitRange) pos -= digitRange;
    }

    int index = Mathf.Clamp(Mathf.RoundToInt(pos), 0, totalFrames - 1);
    if (frames != null && frames.Length > index && frames[index] != null)
        spriteRenderer.sprite = frames[index];
}
}