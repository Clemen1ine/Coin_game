using UnityEngine;

public class mousemovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator animator;
    private SwordAttack swordAttack;

    private bool canMove = true;
    private Vector2 destination;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordAttack = GetComponentInChildren<SwordAttack>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canMove)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetBool("isMoving", true);
        }

        if (Input.GetMouseButtonDown(0) && canMove)
        {
            animator.SetTrigger("SwordAttack");
            LockMovement();
        }

        if (Vector2.Distance(transform.position, destination) > 0.1f)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }

    public void Attack()
    {
        animator.SetTrigger("SwordAttack");
        LockMovement();
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
}