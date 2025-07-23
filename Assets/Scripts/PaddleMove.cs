using UnityEngine;

public abstract class PaddleMove : MonoBehaviour
{
    // This abstract class is responsible for the two paddles - left and right

    #region game objects
    protected Rigidbody2D rb;
    private PongSceneManager pongSceneManager;
    #endregion

    [SerializeField] protected float speed = 20f;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // --> Component of rigidbody paddle

        GameObject obj = GameObject.Find("PongSceneManager"); // --> Searching for game object with "Ball" tag
        if (obj != null)
        {
            pongSceneManager = obj.GetComponent<PongSceneManager>();
        }
    }

    protected void Update()
    { // Ball has "gameStarted" variable that says if game started
        if (pongSceneManager != null && pongSceneManager.gameStarted)
            rb.linearVelocity = Move(); // --> Paddles can move only if game has started
    }

    // Function that makes the paddle move, each paddle has different implementation
    protected abstract Vector2 Move();
    
}
