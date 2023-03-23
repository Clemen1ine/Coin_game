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
    public Collider2D swordCollider;

    private Transform parentTransform;

    public void Start()
    {
        frontAttackOffset = transform.localPosition;
        parentTransform = transform.parent;
    }

    public void AttackFront()
    {
        swordCollider.enabled = true;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void LateUpdate()
    {
        transform.position = parentTransform.position + (Vector3)frontAttackOffset;
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