using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private int damageAmount = 5;

    private bool canAttack = true;
    private Transform _target;

    private void Start()
    {
        _target = FindObjectOfType<PlayerController>().transform;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Attack();
            EnemyAiHelper aiHelper = FindObjectOfType<EnemyAiHelper>();
            if (aiHelper != null)
            {
                aiHelper.SwitchAi();
            }
        }
    }


    public void Attack()
    {
        if (canAttack)
        {
            Health playerHealth = _target.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            canAttack = false;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
