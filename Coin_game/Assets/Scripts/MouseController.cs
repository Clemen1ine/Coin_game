using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject sword;
    public GameObject swordHitbox; 

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private Vector3 swordHitboxOffset; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        swordHitboxOffset = swordHitbox.transform.localPosition; 
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
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
        swordHitbox.transform.position = transform.position + swordHitboxOffset;
    }
}