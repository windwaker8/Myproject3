using UnityEngine;

public class DamageSweepBar : MonoBehaviour
{
    [Header("References")]

    public Transform slider;  // the moving line that sweeps back and forth across the bar
    public Transform greenBar; // full damage zone
    public Transform redBar;   // full track which the slider moves around in

    [Header("Settings")]
    public float sweepSpeed = 1f; // how fast the slider moves, in world units per second

    private float leftBound;   // world X position of the track's left edge
    private float rightBound;  // world X position of the track's right edge
    private int direction = 1; // +1 = moving right, -1 = moving left
    private SpriteRenderer slideSprite; 

    void Start()
    {
        // Derive the sweep bounds from redBar's own width, since redBar
        // visually spans the entire track (greenBar sits inside it)
        SpriteRenderer trackSprite = redBar.GetComponent<SpriteRenderer>(); // gets redBar's SpriteRenderer
        slideSprite = slider.GetComponent<SpriteRenderer>();
        

        float halfWidth = trackSprite.bounds.extents.x; // half of redBar's width, in world units
        leftBound = redBar.position.x - halfWidth;
        rightBound = redBar.position.x + halfWidth;
    }

public void hideSlider()
{
    slideSprite.enabled = false;
}
public void showSlider()
{
    slideSprite.enabled = true;
}

    void Update()
    {
        // Move the slider by speed * direction each frame
        float newX = slider.position.x + direction * sweepSpeed * Time.deltaTime;

        // Bounce off the right edge
        if (newX >= rightBound)
        {
            newX = rightBound;
            direction = -1;
        }
        // Bounce off the left edge
        else if (newX <= leftBound)
        {
            newX = leftBound;
            direction = 1;
        }

        // Apply the new X position to the slider, keeping Y/Z unchanged
        Vector3 pos = slider.position;
        pos.x = newX;
        slider.position = pos;
    }

    // True if the slider is currently overlapping the green zone
    public bool IsInGreenZone() => IsSliderWithin(greenBar);

    // True if the slider is currently overlapping the red zone (the full track)
    public bool IsInRedZone() => IsSliderWithin(redBar);

    //  checks whether the slider's X position falls within a given zone's bounds
    private bool IsSliderWithin(Transform zone)
    {
        SpriteRenderer zoneSprite = zone.GetComponent<SpriteRenderer>();
        float halfWidth = zoneSprite.bounds.extents.x;
        float zoneMin = zone.position.x - halfWidth;
        float zoneMax = zone.position.x + halfWidth;
        return slider.position.x >= zoneMin && slider.position.x <= zoneMax;
    }

    // Returns a damage multiplier based on how close the slider is to green
    // 1.0 (100%, can be customisable) anywhere inside the green zone, tapering down toward 0.2 if at the far end

    public float GetDamageMultiplier()
    {
        // Flat 100% anywhere inside the green zone
        if (IsInGreenZone())
            return 1f;

        //  damage falloff scales with distance from green
        float greenHalfWidth = greenBar.GetComponent<SpriteRenderer>().bounds.extents.x;
        float greenEdgeDistance = Mathf.Abs(slider.position.x - greenBar.position.x) - greenHalfWidth;

    
        // halfWidth - greenHalfWidth = falloff range
        float trackHalfWidth = (rightBound - leftBound) / 2f;
        float maxFalloffDistance = trackHalfWidth - greenHalfWidth;

        // 0 = right at green's edge (near-100%), 1 = at the very edge of the track (worst case)
        float normalizedDistance = Mathf.Clamp01(greenEdgeDistance / maxFalloffDistance);

        // Interpolate between full damage and minimum damage based on how far off the mark the sldier was 
        return Mathf.Lerp(1f, 0.2f, normalizedDistance);
    }
}