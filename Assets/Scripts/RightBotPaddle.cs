using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RightBotPaddle : MonoBehaviour
{
    // This class is responsible for creating a "bot" paddle so you can play singleplayer

    public enum Difficulty { Easy, Medium, Hard }
    private Difficulty currentDifficulty;
    [SerializeField] private Ball ball;

    #region vairables
    private float paddleSpeed;
    private float deadZone;
    private float reactionTime;
    private float errorMargin; //0.5f
    private float nextReactTime = 0f;
    private float desiredYPosition;
    private float failChance;
    #endregion

    // In start we set the difficulty for the game
    void Start()
    {
        SetDifficulty();
    }

    // In update we basically use math to imitate a bot
    void Update()
    {
        if (ball == null) return; // Check if can delete it
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        float vx = ballRb.linearVelocity.x;
        if (Mathf.Approximately(vx, 0f)) return;

        // Wait for next decision slot
        if (Time.time < nextReactTime) return;

        // schedule next think, plus a tiny jitter
        nextReactTime = Time.time + reactionTime + Random.Range(-0.05f, 0.05f);

        // Predict where the ball will cross our X plane
        float dx = transform.position.x - ballRb.transform.position.x;
        float timeToReach = dx / vx;
        float predictedY = ballRb.transform.position.y + ballRb.linearVelocity.y * timeToReach;
         

        if (Random.value < failChance)
        {
            // Bot guesses wrong
            predictedY += Random.Range(-2f, 2f);
        }

        // Add small human-like error
        desiredYPosition = predictedY + Random.Range(-errorMargin, errorMargin);
    }
        // In fixed update we do more math calculation
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

    // Function that sets the difficulty via GameManager.Instance
    private void SetDifficulty()
    {
        float level = GameManager.Instance.GetLevel();

        switch (level)
        {
            case 1:
                currentDifficulty = Difficulty.Easy;
                break;
            case 2:
                currentDifficulty = Difficulty.Medium;
                break;
            case 3:
                currentDifficulty = Difficulty.Hard;
                break;
        }

        // Same paddle speed for all levels
        paddleSpeed = 20f;

        // Adjust decision parameters per difficulty
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                reactionTime = 0.7f;
                errorMargin = 1.5f;
                deadZone = 1.0f;
                failChance = 0.6f;  // if you expose failChance as a field
                break;
            case Difficulty.Medium:
                reactionTime = 0.4f;
                errorMargin = 1.0f;
                deadZone = 0.5f;
                failChance = 0.3f;
                break;
            case Difficulty.Hard:
                reactionTime = 0.2f;
                errorMargin = 0.5f;
                deadZone = 0.2f;
                failChance = 0.1f;
                break;
        }
    }
}
