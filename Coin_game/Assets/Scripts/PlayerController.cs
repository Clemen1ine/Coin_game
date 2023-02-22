using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisonDffset = 0.05f;
    public ContactFilter2D movementFilter;
    
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);

            // Set the animation speed for each axis based on the input values
            animator.SetFloat("WalkSpeedX", movementInput.x);
            animator.SetFloat("WalkSpeedY", movementInput.y);
            animator.SetFloat("WalkSpeedZ", Mathf.Sqrt(Mathf.Pow(movementInput.x, 2) + Mathf.Pow(movementInput.y, 2)));

            if (movementInput.x == 0 && movementInput.y == 0) // Idle animation
            {
                animator.SetFloat("IdleX", 0f);
                animator.SetFloat("IdleY", 0f);
                animator.SetFloat("IdleZ", 0f);
            }
            else if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y)) // Horizontal animation
            {
                animator.SetFloat("IdleX", movementInput.x);
                animator.SetFloat("IdleY", 0f);
                animator.SetFloat("IdleZ", Mathf.Abs(movementInput.x));
            }
            else // Vertical animation
            {
                animator.SetFloat("IdleX", 0f);
                animator.SetFloat("IdleY", movementInput.y);
                animator.SetFloat("IdleZ", Mathf.Abs(movementInput.y));
            }
        }
        else
        {
            animator.SetBool("isMoving", false);

            // Set all idle animations to 0
            animator.SetFloat("IdleX", 0f);
            animator.SetFloat("IdleY", 0f);
            animator.SetFloat("IdleZ", 0f);
        }
    }

    private bool TryMove(Vector2 direction){
        
        int count = rb.Cast(
                
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisonDffset);

        if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
    }
    
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
