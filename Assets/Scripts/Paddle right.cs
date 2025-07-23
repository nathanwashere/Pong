using UnityEngine;

public class Paddleright : PaddleMove
{
    // This class is responisble for moving the left paddle, this paddle moves with up and down arrows
    protected override Vector2 Move()
    {
        float moveY = 0;

        if (Input.GetKey(KeyCode.UpArrow))
            moveY = speed;
        else if (Input.GetKey(KeyCode.DownArrow))
            moveY = -speed;

        return new Vector2(0, moveY);
    }
}
