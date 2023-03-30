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
    private LayerMask layerMask;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        // Set up the layer mask to ignore collisions with a certain layer
        layerMask = LayerMask.GetMask("Ignorecolliders");
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

            // check for obstacles using Physics2D.Raycast and ignore collisions with a certain layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.1f, ~layerMask);
            if (hit.collider == null)
            {
                rb.velocity = direction * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the normal vector of the collision
        Vector2 normal = collision.contacts[0].normal;

        // Calculate a new movement direction based on the collision normal
        Vector2 direction = Vector2.Reflect(rb.velocity.normalized, normal);

        // Apply the new direction to the character's velocity
        rb.velocity = direction * speed * 0.2f;
    }
}


