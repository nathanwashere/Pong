using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private bool isIn = false;
    public bool gameStarted { private set; get; } = false;
    public float rotationSpeed = 360f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float detectGoalDistanceRight = 0.15f;
    [SerializeField] private float detectGoalDistanceLeft = 0.2f;


    public PongMainCanvas mainCanvas;
    private Rigidbody2D rb;

    #region Game objects
    private Vector2 storedVelociy;

    public GameObject wallRight;
    private Vector3 wallRightCord;

    public GameObject wallLeft;
    private Vector3 wallLeftCord;

    public GameObject paddleRight;
    private Vector3 paddleRightCord;

    public GameObject paddleLeft;
    private Vector3 paddleLeftCord;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;  // --> Until the player did not click F the ball stays afloat

        //wallRightCord = wallRight.transform.localPosition;
        //paddleRightCord = paddleRight.transform.localPosition;
        //wallLeftCord = wallLeft.transform.localPosition;
        //paddleLeftCord = paddleLeft.transform.localPosition;

        wallRightCord = wallRight.transform.position;
        paddleRightCord = paddleRight.transform.position;
        wallLeftCord = wallLeft.transform.position;
        paddleLeftCord = paddleLeft.transform.position;
    }

    void Update()
    {
        if (gameStarted) 
        {
            mainCanvas.HideEnterGameSign(); // --> Change later, put it inside main canvas
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime); // --> Rotate it (for the sprite)
            //rb.angularVelocity = 1000f;

            CheckGoal(); // --> Every frame we check if there was goal

            if (Input.GetKeyDown(KeyCode.LeftShift)) // --> We can remove it later
                ResetGame(false);

        }

        else if (!gameStarted && Input.GetKeyDown(KeyCode.F)) // We click F to play
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // --> Changing it back to dynamic for box collider for walls
            gameStarted = true;
            LaunchBall(); // --> Launching the ball in a random direction
        }

    }

    // Function to store velocity to use it later (if we click resume or escape in pause menu)
    public void StoreVelocity()
    {
        storedVelociy = rb.linearVelocity;

        rb.simulated = false; // --> Need it for some reason dont remember
    }

    // Restore the velocity after exiting pause menu via escape or resume
    public void RestoreVelocity()
    {
        rb.simulated = true;
        rb.linearVelocity = storedVelociy;
    }

    // Launching ball in a random direction
    private void LaunchBall()
    {
        rb.linearVelocity = GetDirection();
    }

    // Function that returns a vector to launch the ball
    private Vector2 GetDirection()
    {
        float[] possibleAngles = { 45f, -45f, 135f, -135f };  // --> Four good directions
        float chosenAngle = possibleAngles[Random.Range(0, possibleAngles.Length)];
        float angleRad = chosenAngle * Mathf.Deg2Rad; // --> Some math stuff
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * speed;
    }

    // Function that checks if there is a goal
    private void CheckGoal()
    {
        if (IsInRightSide())
        {
            if (!isIn)
            {
                mainCanvas.AddScore("left"); // --> Updating the UI of score (left)
                isIn = true;
                ResetGame(false); // --> Resetting the position of the ball
            }
        }
        else if (IsInLeftSide())
        {
            if (!isIn)
            {
                mainCanvas.AddScore("right"); // --> Updating the UI of score (right)
                isIn = true;
                ResetGame(false); // --> Resetting the position of the ball
            }
        }
        else if (IsInMiddle())
        {
            if (isIn)
            {
                isIn = false;
            }
        }
    }

    #region check if ball in X region
    private bool IsInMiddle()
    {
        if (transform.position.x > paddleLeftCord.x && transform.position.x < paddleRightCord.x)
            return true;
        return false;
    }
    private bool IsInLeftSide()
    {
        if (transform.position.x < paddleLeftCord.x - detectGoalDistanceLeft)
            return true;
        return false;
    }
    private bool IsInRightSide()
    {
        if (transform.position.x > paddleRightCord.x + detectGoalDistanceRight)
            return true;
        return false;
    }
    #endregion

    // Resetting the game, if reset counter is true we reset the score too
    private void ResetGame(bool resetCounter)
    {
        transform.position = new Vector3(0, 0, 0); // --> resetting the ball position


        if (resetCounter)
        {
            rb.linearVelocity = new Vector2(0, 0);
            mainCanvas.SetCounterLeft(0); mainCanvas.SetCounterRight(0);
        }
        else
        {
            rb.linearVelocity = GetDirection();
            StopRotationOfBallWhenGameEnded(); // --> ResetGame works only if there is a goal, so that means it could end the game
        }

    }

    // Function that when the game has ended makes the ball straight and not in rotation
    public void StopRotationOfBallWhenGameEnded()
    {
        if (GameManager.Instance.IsGameOver)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

}
