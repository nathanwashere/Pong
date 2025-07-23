using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BotPaddle : MonoBehaviour
{
    [Header("Ball References")]
    [Tooltip("Drag your Ball GameObject here")]
    [SerializeField] private Ball ball;

    [Header("Movement Settings")]
    [Tooltip("Maximum paddle movement speed in units per second")]
    [SerializeField] private float paddleSpeed;  //12f

    [Tooltip("Dead zone tolerance: bot won’t move if within this vertical distance to the target")]
    [SerializeField] private float deadZone; //0.2f

    [Header("AI Decision Settings")]
    [Tooltip("Time interval (seconds) between each AI decision (reaction delay)")]
    [SerializeField] private float reactionTime; //0.2f

    [Tooltip("Maximum random offset (± units) added to the aim target to simulate error")]
    [SerializeField] private float errorMargin; //0.5f

    private float nextReactTime = 0f;
    private float desiredYPosition;

    void Start()
    {
        SetDifficulty();
    }
    void Update()
    {
        if (ball == null) return; // Check if can delete it

        // Wait for next decision slot
        if (Time.time < nextReactTime) return;
        // schedule next think, plus a tiny jitter
        nextReactTime = Time.time + reactionTime + Random.Range(-0.05f, 0.05f);

        // Predict where the ball will cross our X plane
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        float vx = ballRb.linearVelocity.x;
        if (Mathf.Approximately(vx, 0f)) return;  // avoid divide by zero

        // Distance in X between paddle and ball
        float dx = transform.position.x - ball.transform.position.x;
        float timeToReach = dx / vx;

        // Where the ball will be vertically
        float predictedY = ball.transform.position.y + ballRb.linearVelocity.y * timeToReach;

        // Add some human like error
        desiredYPosition = predictedY + Random.Range(-errorMargin, errorMargin);
    }

    void FixedUpdate()
    {
        // Move toward targetY
        float deltaY = desiredYPosition - transform.position.y;

        // Only move if outside our dead zone
        if (Mathf.Abs(deltaY) > deadZone)
        {
            float dir = Mathf.Sign(deltaY);
            float moveThisFrame = paddleSpeed * Time.fixedDeltaTime;
            moveThisFrame = Mathf.Min(moveThisFrame, Mathf.Abs(deltaY));
            transform.Translate(Vector3.up * dir * moveThisFrame);
        }
    }

    private void SetDifficulty()
    {
        float[] difficulty = GameManager.Instance.GetLevel();
        paddleSpeed = difficulty[0];
        reactionTime = difficulty[1];
        errorMargin = difficulty[2];
        deadZone = difficulty[3];
    }
}
