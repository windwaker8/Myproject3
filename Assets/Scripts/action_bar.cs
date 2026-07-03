using UnityEngine;

public class DamageSweepBar : MonoBehaviour
{
    [Header("References")]
   
    public Transform slider;
    public Transform greenBar;
    public Transform redBar;

    [Header("Settings")]
    public float sweepSpeed = 1f; // units per second (world space, likely much smaller than 200)

    private float leftBound;
    private float rightBound;
    private int direction = 1;

    void Start()
    {
       
        SpriteRenderer trackSprite = redBar.GetComponent<SpriteRenderer>();
        float halfWidth = trackSprite.bounds.extents.x;
        leftBound = redBar.position.x - halfWidth;
        rightBound = redBar.position.x + halfWidth;
    }

    void Update()
    {
        float newX = slider.position.x + direction * sweepSpeed * Time.deltaTime;

        if (newX >= rightBound)
        {
            newX = rightBound;
            direction = -1;
        }
        else if (newX <= leftBound)
        {
            newX = leftBound;
            direction = 1;
        }

        Vector3 pos = slider.position;
        pos.x = newX;
        slider.position = pos;
    }

    public bool IsInGreenZone() => IsSliderWithin(greenBar);
    public bool IsInRedZone() => IsSliderWithin(redBar);

    private bool IsSliderWithin(Transform zone)
    {
        SpriteRenderer zoneSprite = zone.GetComponent<SpriteRenderer>();
        float halfWidth = zoneSprite.bounds.extents.x;
        float zoneMin = zone.position.x - halfWidth;
        float zoneMax = zone.position.x + halfWidth;
        return slider.position.x >= zoneMin && slider.position.x <= zoneMax;
    }

    public float GetDamageMultiplier()
{
    // Flat 100% anywhere inside the green zone
    if (IsInGreenZone())
        return 1f;

    // Outside green: falloff based on distance from green zone's edge
    float greenHalfWidth = greenBar.GetComponent<SpriteRenderer>().bounds.extents.x;
    float greenEdgeDistance = Mathf.Abs(slider.position.x - greenBar.position.x) - greenHalfWidth;

    float trackHalfWidth = (rightBound - leftBound) / 2f;
    float maxFalloffDistance = trackHalfWidth - greenHalfWidth; // distance from green's edge to track's edge

    float normalizedDistance = Mathf.Clamp01(greenEdgeDistance / maxFalloffDistance);
    return Mathf.Lerp(1f, 0.2f, normalizedDistance);
}
}