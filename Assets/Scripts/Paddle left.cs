using UnityEngine;

public class Paddleleft : PaddleMove
{
    // This class is responisble for moving the right paddle, this paddle moves with W and S
    protected override Vector2 Move()
    {   
        float moveY = 0;

        if (Input.GetKey("w"))
            moveY = speed;
        else if (Input.GetKey("s"))
            moveY = -speed;

        return new Vector2(0, moveY);
    }
}
