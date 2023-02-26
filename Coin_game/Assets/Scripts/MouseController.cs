using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb2d;
    private Vector2 movementInput;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get the direction of the mouse from the player position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        // Update the movement input based on the mouse direction
        movementInput = new Vector2(direction.x, direction.y);
    }

    private bool TryMove(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, moveSpeed * Time.fixedDeltaTime);

        if (hit.collider == null)
        {
            rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else if (hit.collider.CompareTag("SmallCoin") || hit.collider.CompareTag("BigCoin"))
        {
            rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        // if the player has moved the mouse
        if (movementInput != Vector2.zero)
        {
            // try to move the character in the chosen direction
            bool success = TryMove(movementInput);

            // if failed to move in chosen direction, try to move in other directions
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }
}