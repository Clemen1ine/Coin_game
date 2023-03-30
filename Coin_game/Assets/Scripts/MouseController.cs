using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject sword;

    private Animator animator;
    private Vector2 targetPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            // set the walking animation to true
            animator.SetBool("isMoving", true);
        }
        else
        {
            // set the walking animation to false
            animator.SetBool("isMoving", false);
        }
    }
}