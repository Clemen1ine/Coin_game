using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 _destination;
    private bool _isMoving = false;
    private float _attackTime = .25f;
    private float _attackCounter = .25f;
    private bool _isAttacking;
    private Vector2 _startPosition;
    private float _minimumAttackDistance = 0.1f;
    private Vector2 _moveDirection = Vector2.zero;
    private float _lastMoveX;
    private float _lastMoveY;

    private void Start()
    {
        _startPosition = rb.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            _destination = mousePos;
            Vector2 direction = (_destination - (Vector2)transform.position).normalized;

            // Set moveDirection based on the difference between current position and destination
            _moveDirection = (_destination - rb.position).normalized;

            // Set animator values based on moveDirection
            animator.SetFloat("Horizontal", _moveDirection.x);
            animator.SetFloat("Vertical", _moveDirection.y);
            animator.SetFloat("Speed", 1f);

            _isMoving = true;
        }

        if (_isMoving && Vector2.Distance(rb.position, _destination) < 0.1f)
        {
            _isMoving = false;
            animator.SetFloat("Speed", 0f);
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

        if (_isMoving)
        {
            // Update animator values based on moveDirection
            animator.SetFloat("Horizontal", _moveDirection.x);
            animator.SetFloat("Vertical", _moveDirection.y);

            // Save the current moveDirection
            _lastMoveX = _moveDirection.x;
            _lastMoveY = _moveDirection.y;

            // Move the rigidbody in the direction of moveDirection
            rb.MovePosition(rb.position + _moveDirection * moveSpeed * Time.deltaTime);
        }
    }
    
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Vector2 direction = (_destination - (Vector2)transform.position).normalized;
            Vector2 movement = new Vector2(
                direction.x * moveSpeed * Time.fixedDeltaTime,
                direction.y * moveSpeed * Time.fixedDeltaTime
            );
            rb.MovePosition(rb.position + movement);

            // Set animator values based on LastMoveX and LastMoveY
            if (_lastMoveX != 0f) {
                animator.SetFloat("Horizontal", _lastMoveX);
            }
            if (_lastMoveY != 0f) {
                animator.SetFloat("Vertical", _lastMoveY);
            }
        }
    }
}
