using UnityEngine;

public class DualControl : MonoBehaviour
{
    public float baseMoveSpeed = 5f; // The base movement speed
    public float shiftMultiplier = 2f; // Multiplier applied to moveSpeed when shift is held down
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private float attackTime = .25f;
    private float attackCounter = .25f;
    private bool isAttacking;
    private Vector2 startPosition;
    private float minimumAttackDistance = 0.1f;

    private void Start()
    {
        startPosition = rb.position;
    }

    private void Update()
    {
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("IsAttacking", false);
                isAttacking = false;
            }
        }
        
        // Mouse Movement
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            // Get mouse position in world space
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate movement direction based on the difference between mouse position and current position
            movement = mousePosition - rb.position;

            // Normalize the movement vector to prevent faster diagonal movement
            movement.Normalize();
        }
        // Keyboard Movement
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Normalize the movement vector to prevent faster diagonal movement
            movement.Normalize();
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x != 0 || movement.y != 0)
        {
            // Set the LastMoveX and LastMoveY based on the greater absolute value
            if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
            {
                animator.SetFloat("LastMoveX", Mathf.Sign(movement.x));
                animator.SetFloat("LastMoveY", 0);
            }
            else
            {
                animator.SetFloat("LastMoveX", 0);
                animator.SetFloat("LastMoveY", Mathf.Sign(movement.y));
            }
        }
        else
        {
            // Stop the character and make them face the side based on LastMoveX and LastMoveY
            animator.SetFloat("Speed", 0);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isAttacking && Vector2.Distance(rb.position, startPosition) >= minimumAttackDistance)
            {
                attackCounter = attackTime;
                animator.SetBool("IsAttacking", true);
                isAttacking = true;

                // Reset movement input while attacking
                movement = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        float currentMoveSpeed = baseMoveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentMoveSpeed *= shiftMultiplier;
        }

        if (!isAttacking)
        {
            Vector2 movementVelocity = movement * currentMoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movementVelocity);
        }
    }
}