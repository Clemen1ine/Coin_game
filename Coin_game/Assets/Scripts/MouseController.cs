using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject sword;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 targetPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            rb.velocity = direction * speed;

            // set the walking animation to true
            animator.SetBool("isMoving", true);
        }
        else
        {
            rb.velocity = Vector2.zero;

            // set the walking animation to false
            animator.SetBool("isMoving", false);
        }
    }
}