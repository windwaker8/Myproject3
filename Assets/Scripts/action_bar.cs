using UnityEngine;

public class DamageSweepBar : MonoBehaviour
{
    [Header("References")]
    public RectTransform barArea;   // the overall track width -- can be "action meter" itself
    public RectTransform slider;    // the moving line
    public RectTransform greenBar;  // green zone bounds
    public RectTransform redBar;    // red zone bounds (optional, for explicit checking)

    [Header("Settings")]
    public float sweepSpeed = 200f; // pixels per second

    private float linePos;
    private int direction = 1;

    void Update()
    {
        float barWidth = barArea.rect.width;

        linePos += direction * sweepSpeed * Time.deltaTime;

        // Bounce back and forth across the bar
        if (linePos >= barWidth)
        {
            linePos = barWidth;
            direction = -1;
        }
        else if (linePos <= 0f)
        {
            linePos = 0f;
            direction = 1;
        }

        // Move the slider to match
        Vector2 pos = slider.anchoredPosition;
        pos.x = linePos;
        slider.anchoredPosition = pos;
    }

    public bool IsInGreenZone()
    {
        return IsSliderWithin(greenBar);
    }

    public bool IsInRedZone()
    {
        return IsSliderWithin(redBar);
    }

    private bool IsSliderWithin(RectTransform zone)
    {
        float zoneMin = zone.anchoredPosition.x - (zone.rect.width / 2f);
        float zoneMax = zone.anchoredPosition.x + (zone.rect.width / 2f);
        return linePos >= zoneMin && linePos <= zoneMax;
    }
}