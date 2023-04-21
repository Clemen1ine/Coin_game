using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private float attackTime = .25f;
    private float attackCounter = .25f;
    private bool IsAttacking;
    private Vector2 startPosition;
    private float minimumAttackDistance = 0.1f;

    private void Start()
    {
        startPosition = rb.position;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
        {
            animator.SetFloat("LastMoveX", movement.x);
            animator.SetFloat("LastMoveY", movement.y);
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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime); 
    }
}