using UnityEngine;

public class RollingDigit : MonoBehaviour
{
    public float frameWidth = 6.42f;   // 924 ÷ (12 digits × 12 frames) = ~6.42 px
    public int framesPerDigit = 12;    // 12 frames per number
    public float hpPerSecond = 30f;    // roll speed (HP per second)

    private float pixelsPerSecond;
    public int currentDigit;
    public int targetDigit;
    private int currentFrame;

    void Start()
    {
        pixelsPerSecond = frameWidth * hpPerSecond;
    }

    void Update()
    {
        if (currentDigit != targetDigit)
        {
            float direction = Mathf.Sign(targetDigit - currentDigit);
            transform.localPosition += new Vector3(-direction * pixelsPerSecond * Time.deltaTime, 0, 0);

            float moved = Mathf.Abs(transform.localPosition.x + currentDigit * framesPerDigit * frameWidth);
            currentFrame = (int)(moved / frameWidth);

            if (currentFrame >= framesPerDigit)
            {
                if (targetDigit > currentDigit) currentDigit++;
                else if (targetDigit < currentDigit) currentDigit--;
                currentFrame = 0;
                transform.localPosition = new Vector3(-currentDigit * framesPerDigit * frameWidth, transform.localPosition.y, transform.localPosition.z);
            }
        }
    }
}
