using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
         // получаем ввод от игрока для передвижения персонажа
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
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
        // если игрок ввел направление движения
        if (movementInput != Vector2.zero) 
        {
            // попітка переместить персонажа в вабраном направлении
            bool success = TryMove(movementInput); 

            // если не удалось переместить персонажа в выбранном направлении, то перемещаем в другом
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