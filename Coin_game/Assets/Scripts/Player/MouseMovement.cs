using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float baseMoveSpeed = 5f; // The base movement speed
    public float shiftMultiplier = 2f; // Multiplier applied to moveSpeed when shift is held down
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 _movement;
    private float _attackTime = 0.25f;
    private float _attackCounter = 0.25f;
    private bool _isAttacking;
    private Vector2 _startPosition;
    private float _minimumAttackDistance = 0.1f;

    private void Start()
    {
        _startPosition = rb.position;
    }

    private void Update()
    {
        if (!_isAttacking)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _movement = (mousePosition - transform.position).normalized;
            }
            else
            {
                _movement = Vector2.zero;
            }
        }
        else
        {
            _movement = Vector2.zero;
        }

        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.magnitude);

        if (_movement.magnitude > 0)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("LastMoveX", _movement.x);
            animator.SetFloat("LastMoveY", _movement.y);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (_isAttacking)
        {
            rb.velocity = Vector2.zero;
            _attackCounter -= Time.deltaTime;
            if (_attackCounter <= 0)
            {
                animator.SetBool("IsAttacking", false);
                _isAttacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!_isAttacking && Vector2.Distance(rb.position, _startPosition) >= _minimumAttackDistance)
            {
                _attackCounter = _attackTime;
                animator.SetBool("IsAttacking", true);
                _isAttacking = true;

                // Reset movement input while attacking
                _movement = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isAttacking)
        {
            float currentMoveSpeed = baseMoveSpeed;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentMoveSpeed *= shiftMultiplier;
            }

            Vector2 movementVelocity = _movement * currentMoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movementVelocity);
        }
    }
}
