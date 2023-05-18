using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 destination;
    private bool isMoving = false;
    private float attackTime = .25f;
    private float attackCounter = .25f;
    private bool IsAttacking;
    private Vector2 startPosition;
    private float minimumAttackDistance = 0.1f;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 lastMoveDirection = Vector2.zero;

    private void Start()
    {
        startPosition = rb.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            destination = mousePos;
            moveDirection = (destination - rb.position).normalized;

            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", 1f);

            if (!isMoving)
            {
                lastMoveDirection = moveDirection;
            }

            isMoving = true;
        }

        if (isMoving && Vector2.Distance(rb.position, destination) < 0.1f)
        {
            isMoving = false;
            animator.SetFloat("Speed", 0f);
        }

        if (IsAttacking)
        {
            rb.velocity = Vector2.zero;
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("IsAttacking", false);
                IsAttacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Vector2.Distance(rb.position, startPosition) >= minimumAttackDistance)
            {
                attackCounter = attackTime;
                animator.SetBool("IsAttacking", true);
                IsAttacking = true;
            }
        }

        if (isMoving)
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);

            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }
    
    private void FixedUpdate()
    {
        if (!isMoving)
        {
            if (lastMoveDirection.x != 0f)
            {
                animator.SetFloat("Horizontal", lastMoveDirection.x);
            }
            if (lastMoveDirection.y != 0f)
            {
                animator.SetFloat("Vertical", lastMoveDirection.y);
            }
        }
    }
}
