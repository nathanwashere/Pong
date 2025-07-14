using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private bool isIn = false;
    public bool gameStarted { private set; get; } = false;

    public float rotationSpeed = 360f;
    public PongMainCanvas mainCanvas;

    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float detectGoalDistanceRight = 0.15f;
    [SerializeField] private float detectGoalDistanceLeft = 0.2f;

    private Vector2 storedVelociy;


    public GameObject wallRight;
    private Vector3 wallRightCord;

    public GameObject wallLeft;
    private Vector3 wallLeftCord;

    public GameObject paddleRight;
    private Vector3 paddleRightCord;

    public GameObject paddleLeft;
    private Vector3 paddleLeftCord;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;

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
            mainCanvas.HideEnterGameSign();
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            //rb.angularVelocity = 1000f;

            CheckGoal();

            if (Input.GetKeyDown(KeyCode.LeftShift))
                ResetGame(false);

        }

        else if (!gameStarted && Input.GetKeyDown(KeyCode.F))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            gameStarted = true;
            LaunchBall();
        }

    }

    public void StoreVelocity()
    {
        storedVelociy = rb.linearVelocity;

        rb.simulated = false;
    }
    public void RestoreVelocity()
    {
        rb.simulated = true;
        rb.linearVelocity = storedVelociy;
    }
    private void LaunchBall()
    {
        rb.linearVelocity = GetDirection();
    }
    private Vector2 GetDirection()
    {
        float[] possibleAngles = { 45f, -45f, 135f, -135f };  // Four good directions
        float chosenAngle = possibleAngles[Random.Range(0, possibleAngles.Length)];
        float angleRad = chosenAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized * speed;
    }
    private void CheckGoal()
    {
        if (IsInRightSide())
        {
            if (!isIn)
            {
                mainCanvas.AddScore("left");
                isIn = true;
                ResetGame(false);
            }
        }
        else if (IsInLeftSide())
        {
            if (!isIn)
            {
                mainCanvas.AddScore("right");
                isIn = true;
                ResetGame(false);
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
    private void ResetGame(bool resetCounter)
    {
        transform.position = new Vector3(0, 0, 0);


        if (resetCounter)
        {
            rb.linearVelocity = new Vector2(0, 0);
            mainCanvas.SetCounterLeft(0); mainCanvas.SetCounterRight(0);
        }
        else
        {
            rb.linearVelocity = GetDirection();
            CheckEndGame();
        }

    }
    public void CheckEndGame()
    {
        // When game ends, this function makes the ball straight and not in rotation
        if (GameManager.Instance.IsGameOver)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

}
