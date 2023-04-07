using UnityEngine;

public class RB2Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 MoveInput;
    private Vector2 MoveVelocity;
    private Animator animator;
    private SwordAttack swordAttack;

    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordAttack = GetComponentInChildren<SwordAttack>();
    }

    private void Update()
    {
        MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MoveVelocity = MoveInput.normalized * speed;
        
        if (MoveVelocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            animator.SetTrigger("SwordAttack");
            LockMovement();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);
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
        print("attak");
        LockMovement();
        swordAttack.AttackFront();
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}