using UnityEngine;

public abstract class PaddleMove : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float speed = 10f;
    public Ball ball;
    public float minY = -5.2f;  
    public float maxY = 5.2f;    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject objWithB = GameObject.Find("Ball");
        if (objWithB != null)
        {
            ball = objWithB.GetComponent<Ball>();
        }

    }

    // Update is called once per frame
    protected void Update()
    {
        if (ball != null && ball.gameStarted)
            rb.linearVelocity = Move();     
    }

    protected abstract Vector2 Move();
    
}
