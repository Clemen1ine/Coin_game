using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private Animator animator;
    private Vector2 lastMoveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveTowardTarget(Vector2 targetPosition)
    {
        Vector2 currentPosition = transform.position;
        Vector2 moveDirection = (targetPosition - currentPosition).normalized;

        rb.velocity = moveDirection * speed;

        UpdateAnimator(moveDirection);
    }

    private void UpdateAnimator(Vector2 moveDirection)
    {
        if (animator != null)
        {
            animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.y);

            if (moveDirection != Vector2.zero)
            {
                lastMoveDirection = moveDirection;
            }

            animator.SetBool("isMoving", moveDirection != Vector2.zero);
        }
    }
}
