using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 _movement;
    private float _attackTime = .25f;
    private float _attackCounter = .25f;
    private bool _isAttacking;
    private Vector2 _startPosition;
    private float _minimumAttackDistance = 0.1f;

    private void Start()
    {
        _startPosition = rb.position;    
    }

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);

        if (_movement.x == 1 || _movement.x == -1 || _movement.y == 1 || _movement.y == -1)
        {
            animator.SetFloat("LastMoveX", _movement.x);
            animator.SetFloat("LastMoveY", _movement.y);
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
            if (Vector2.Distance(rb.position, _startPosition) >= _minimumAttackDistance)
            {
                _attackCounter = _attackTime;
                animator.SetBool("IsAttacking", true);
                _isAttacking = true;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime); 
    }
}