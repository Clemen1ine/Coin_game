using System;
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
    public float damage = 3;
    private Vector2 frontAttackOffset;
    private Vector2 attackStartPosition;
    public Collider2D swordCollider;

    public void Start()
    {
        frontAttackOffset = transform.position;
    }

    public void AttackFront()
    {
        swordCollider.enabled = true;
        attackStartPosition = transform.position;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
        transform.position = attackStartPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}