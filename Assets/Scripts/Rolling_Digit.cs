using UnityEngine;

public class RollingDigit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;   // assign in Inspector

    // Drag all 132 sliced sprites here in order: btlNums_0 ... btlNums_131
    // Index 0-119  = digits 0-9, 12 sub-frames each (digit*12 + subframe)
    // Index 120-131 = blank slot, 12 sub-frames
    public Sprite[] frames;

    public int framesPerDigit = 12;         // sub-frames per digit (odometer roll)
    public int blankDigitIndex = 10;        // the blank "digit" slot (comes after 9)
    private int totalDigitSlots = 11;       // 0-9 plus blank

    public float framesPerSecond = 60f;     // visual scrub speed along the strip
                                             // (independent of HP tick rate -- purely cosmetic speed)

    public int currentDigit = 0;            // -1 means blank. Public so it can be seeded/inspected.
    public int targetDigit = 0;             // -1 means blank

    // Single source of truth: continuous position along the whole strip, in frame units (0..totalFrames-1).
    private float currentFramePos;
    private int totalFrames;                // totalDigitSlots * framesPerDigit

    void Awake()
    {
        totalFrames = totalDigitSlots * framesPerDigit;

        if (frames == null || frames.Length != totalFrames)
        {
            Debug.LogError($"{name}: RollingDigit expects {totalFrames} frames assigned, but found {(frames == null ? 0 : frames.Length)}.");
        }

        SnapToCurrent();
    }

    // Instantly jumps to match currentDigit, no rolling animation.
    // Also forces targetDigit to match, so Update() won't immediately roll away from it.
    public void SnapToCurrent()
    {
        int slot = currentDigit >= 0 ? currentDigit : blankDigitIndex;
        currentFramePos = slot * framesPerDigit;
        targetDigit = currentDigit;
        ApplySprite();
    }

    void Update()
    {
        int targetSlot = targetDigit >= 0 ? targetDigit : blankDigitIndex;
        int currentSlot = currentDigit >= 0 ? currentDigit : blankDigitIndex;

        if (currentSlot == targetSlot)
            return; // already there, nothing to roll

        float targetFramePos = targetSlot * framesPerDigit; // land on sub-frame 0 of target digit
        float direction = Mathf.Sign(targetFramePos - currentFramePos);

        float step = framesPerSecond * Time.deltaTime; // frames to advance this tick
        currentFramePos += direction * step;

        bool reached = direction > 0 ? currentFramePos >= targetFramePos
                                      : currentFramePos <= targetFramePos;

        if (reached)
        {
            currentFramePos = targetFramePos;
            currentDigit = (targetSlot == blankDigitIndex) ? -1 : targetSlot;
        }

        ApplySprite();
    }

    private void ApplySprite()
    {
        int frameIndex = Mathf.Clamp(Mathf.RoundToInt(currentFramePos), 0, totalFrames - 1);
        spriteRenderer.sprite = frames[frameIndex];
    }
}