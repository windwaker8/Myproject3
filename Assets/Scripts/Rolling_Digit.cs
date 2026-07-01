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
        targetFramePos = targetSlot * framesPerDigit;

        // Determine roll direction based on digit value change
        int currentSlot = currentDigit >= 0 ? currentDigit : blankDigitIndex;
        if (targetSlot != currentSlot)
            isRolling = true;
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
            currentFramePos = targetFramePos;
            currentDigit = targetDigit;
            isRolling = false;
        }

        ApplySprite();
    }

    private void ApplySprite()
    {
        int index = Mathf.Clamp(Mathf.RoundToInt(currentFramePos), 0, totalFrames - 1);
        if (frames != null && frames.Length > index && frames[index] != null)
            spriteRenderer.sprite = frames[index];
    }
}