using UnityEngine;

public abstract class PaddleMove : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float speed = 10f;
    public Ball ball;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject objWithB = GameObject.Find("Ball");
        if (objWithB != null)
        {
            ball = objWithB.GetComponent<Ball>();
        }

    }


    protected void Update()
    {
        if (ball != null && ball.gameStarted)
            rb.linearVelocity = Move();     
    }

    protected abstract Vector2 Move();
    
}
