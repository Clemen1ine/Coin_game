using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb2d; 
    private Vector2 movementInput;
    private Animator animator;
    private SwordAttack swordAttack;

    private bool canMove = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordAttack = GetComponentInChildren<SwordAttack>();
    }

    private void Update()
    {
        // получаем ввод от игрока для передвижения персонажа
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private bool TryMove(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, moveSpeed * Time.fixedDeltaTime);

        if (hit.collider == null)
        {
            rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else if (hit.collider.CompareTag("SmallCoin") || hit.collider.CompareTag("BigCoin") || hit.collider.CompareTag("Enemy"))
        {
            rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // если игрок ввел направление движения
            if (movementInput != Vector2.zero)
            {
                // попытка переместить персонажа в выбранном направлении
                bool success = TryMove(movementInput);

                // если не удалось переместить персонажа в выбранном направлении, то перемещаем в другом
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            swordAttack.attackDirection = SwordAttack.AttackDirection.front;
        }
    }

    void OnFire()
    {
        animator.SetTrigger("SwordAttack");
    }

    public void EndAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
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