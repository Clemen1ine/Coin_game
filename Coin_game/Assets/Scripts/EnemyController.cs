using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private Transform _target;
    
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    public Transform homePos;

    private Rigidbody2D _rb;

    private void Start()
    {
        homePos.parent = null;
        _animator = GetComponent<Animator>();
        _target = FindObjectOfType<PlayerController>().transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector3.Distance(_target.position, transform.position) <= maxRange && Vector3.Distance(_target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if(Vector3.Distance(_target.position, transform.position) >= maxRange)
        {
            GoHome();
        }
    }

    public void FollowPlayer()
    {
        _animator.SetBool("isMoving", true);

        // Calculate direction towards the player
        Vector2 direction = (_target.position - transform.position).normalized;

        // Check if player is close to a collider with a rigidbody component
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRange);
        if (hit.collider != null && hit.distance < minRange && hit.rigidbody != null)
        {
            // Player is close to a collider with a rigidbody component, adjust direction of movement
            Vector2 collisionNormal = hit.normal;
            direction = Vector2.Reflect(direction, collisionNormal);
        }

        // Calculate next position
        Vector2 nextPos = _rb.position + direction * speed * Time.fixedDeltaTime;

        // Check for collisions with colliders that have a rigidbody component
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Default")); // Change "Default" to the layer that your colliders are on
        Collider2D[] colliders = new Collider2D[10];
        int numColliders = _rb.OverlapCollider(contactFilter, colliders);
        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i].attachedRigidbody != null)
            {
                Vector2 closestPoint = colliders[i].ClosestPoint(nextPos);
                if (Vector2.Distance(nextPos, closestPoint) < 0.1f)
                {
                    // Next position is inside a collider with a rigidbody component, move enemy to just before the collider
                    _rb.MovePosition(closestPoint - direction * 0.1f);
                    return;
                }
            }
        }

        // Set animator parameters and move enemy
        _animator.SetFloat("moveX", direction.x);
        _animator.SetFloat("moveY", direction.y);
        _rb.MovePosition(nextPos);
    }
    
    public void GoHome()
    {
        _animator.SetFloat("moveX",((Vector2)homePos.position - (Vector2)transform.position).x);
        _animator.SetFloat("moveY",((Vector2)homePos.position - (Vector2)transform.position).y);

        if (Vector3.Distance(transform.position, homePos.position) < 0.05f)
        {
            _animator.SetBool("isMoving", false);
            _rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = ((Vector2)homePos.position - (Vector2)transform.position).normalized;
            _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}