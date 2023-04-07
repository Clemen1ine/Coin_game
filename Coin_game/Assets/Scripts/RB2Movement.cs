using UnityEngine;

public class RB2Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 MoveInput;
    private Vector2 MoveVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MoveVelocity = MoveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);
    }
}