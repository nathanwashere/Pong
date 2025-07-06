using UnityEngine;

public class Ground : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10f;
    private float movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

 
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movement, 0) * speed;
    }
}
