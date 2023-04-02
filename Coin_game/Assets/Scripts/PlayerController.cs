using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask obstacleMask;
    public float raycastDistance = 0.1f;

    private Vector2 movementDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private SwordAttack swordAttack;

    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordAttack = GetComponentInChildren<SwordAttack>();
    }

    void Update()
    {
        // Read input from horizontal and vertical axis
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction from input
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // Check for obstacles in movement direction using raycasts
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, raycastDistance, obstacleMask);

        if (hit.collider == null)
        {
            // If there are no obstacles in the way, move the player
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.deltaTime);

            // Set walking animation if player is moving
            if (movementDirection != Vector2.zero)
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

        // Check for sword attack input
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
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
            Debug.Log("Player crashed into " + other.gameObject.name);
        }
    }

    void OnFire()
    {
        animator.SetTrigger("SwordAttack");
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

    public void PerformSwordAttack()
    {
        print("attak");
        LockMovement();
        swordAttack.AttackFront();
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void MoveObjectWithPlayer(GameObject obj)
    {
        Vector2 offset = obj.transform.position - transform.position;
        
        obj.transform.position = rb.position + offset;
    }
}
