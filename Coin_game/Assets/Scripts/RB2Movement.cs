using UnityEngine;

public class RB2Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator animator;
    private SwordAttack swordAttack;
    private bool canMove = true;
    private SpriteRenderer spriteRenderer;
    private Transform characterHeadTransform; // Separate transform for the character's head

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordAttack = GetComponentInChildren<SwordAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterHeadTransform = transform.Find("Character/Head"); // Update with the correct path to the character's head
    }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if (moveVelocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (moveInput.x > 0)
        {
            animator.SetBool("isFacingRight", true);
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            animator.SetBool("isFacingRight", false);
            spriteRenderer.flipX = true;
        }

        if (Input.GetMouseButtonDown(0) && canMove)
        {
            animator.SetTrigger("SwordAttack");
            LockMovement();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        print("attack");
        animator.SetTrigger("SwordAttack");
        LockMovement();
    }

    void OnFire()
    {
        animator.SetTrigger("SwordAttack");
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

    public void PerformSwordAttack()
    {
        print("attack");
        LockMovement();
        swordAttack.AttackFront();
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}