using UnityEngine;

public class Paddleleft : PaddleMove
{
    protected override Vector2 Move()
    {   
        float moveY = 0;

        if (Input.GetKey("w"))
            moveY = speed;
        else if (Input.GetKey("s"))
            moveY = -speed;

        return new Vector2(0, moveY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            Debug.Log("I have touched the ground!");
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
}
