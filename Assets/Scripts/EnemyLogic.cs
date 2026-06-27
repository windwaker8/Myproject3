using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRadius = 5f;
    public Vector2 wanderDurationRange = new Vector2(1f, 3f); // walk between 1–3 seconds
    public Vector2 pauseDurationRange = new Vector2(0.5f, 2f); // pause between 0.5–2 seconds

    private Rigidbody2D rb;
    private Vector2 wanderDirection;
    private float stateTimer;
    private bool isPaused;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wanderDirection = Random.insideUnitCircle.normalized;
        stateTimer = Random.Range(wanderDurationRange.x, wanderDurationRange.y);
        isPaused = false;
    }

    void FixedUpdate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        speedCheck(player); // ✅ speed adjusts before deciding chase/wander

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < detectionRadius)
            Chase(player);
        else
            Wander();
    }

    void Chase(GameObject player)
    {
        Vector2 toPlayer = (player.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + toPlayer * moveSpeed * Time.fixedDeltaTime);
    }

    void Wander()
    {
        stateTimer -= Time.fixedDeltaTime;

        if (isPaused)
        {
            if (stateTimer <= 0)
            {
                wanderDirection = Random.insideUnitCircle.normalized;
                stateTimer = Random.Range(wanderDurationRange.x, wanderDurationRange.y);
                isPaused = false;
            }
        }
        else
        {
            rb.MovePosition(rb.position + wanderDirection * moveSpeed * Time.fixedDeltaTime);

            if (stateTimer <= 0)
            {
                stateTimer = Random.Range(pauseDurationRange.x, pauseDurationRange.y);
                isPaused = true;
            }
        }
    }

    void speedCheck(GameObject player)
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < detectionRadius)
            moveSpeed = 4f; // chasing speed
        else
            moveSpeed = 3f; // wandering speed
    }
}
