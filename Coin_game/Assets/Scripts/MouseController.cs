using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask obstacleMask;
    public float raycastDistance = 0.1f;

    private Vector2 movementDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private bool canMove = true;
    private SwordAttack swordAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordAttack = GetComponentInChildren<SwordAttack>();
    }

    void Update()
    {
        // Check if the right mouse button is pressed
        if (Input.GetMouseButton(1) && canMove)
        {
            // Calculate the direction from the player's position to the cursor
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();

            // Check for obstacles in movement direction using raycasts
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, obstacleMask);

            // Check the distance between the player and the cursor
            float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance <= 0.04f)
            {
                // Stop moving and walking animation if the player is close to the cursor
                rb.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
            else if (hit.collider == null)
            {
                // If there are no obstacles in the way, move the player
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

                // Set walking animation if player is moving
                if (direction != Vector2.zero)
                {
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }
            else
            {
                // If there is an obstacle, don't move and stop walking animation
                rb.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            // Stop walking animation if the player is not moving
            animator.SetBool("isMoving", false);
        }

        // Check for sword attack input
        if (Input.GetKeyDown(KeyCode.Mouse0) && canMove)
        {
            animator.SetTrigger("SwordAttack");
            LockMovement();
        }
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        // When the player collides with an obstacle, stop its movement and print a message
        if (other.gameObject.layer == obstacleMask)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Mouse crashed into " + other.gameObject.name);
        }
    }

    public void EndAttack()
    {
        UnlockMovement();

        // Check if swordAttack has been initialized before calling StopAttack()
        if (swordAttack != null)
        {
            swordAttack.StopAttack();
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void PerformSwordAttack()
    {
        LockMovement();
        swordAttack.AttackFront();
    }
}
