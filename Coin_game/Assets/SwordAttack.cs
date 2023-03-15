using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public enum AttackDirection
    {
        front
    }

    public AttackDirection attackDirection;

    private Vector2 frontAttackOffset;
    private Collider2D swordCollider;

    public void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
        frontAttackOffset = transform.position;
    }

    public void AttackFront()
    {
        swordCollider.enabled = true;
        transform.position = frontAttackOffset;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }
}