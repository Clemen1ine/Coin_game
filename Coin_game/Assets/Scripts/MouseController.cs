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
        if (Input.GetMouseButton(1) && canMove)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, obstacleMask);
            
            float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance <= 0.04f)
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
            else if (hit.collider == null)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
                
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
                rb.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && canMove)
        {
            animator.SetTrigger("SwordAttack");
            LockMovement();
        }
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == obstacleMask)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Mouse crashed into " + other.gameObject.name);
        }
    }

    public void EndAttack()
    {
        UnlockMovement();
        
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
