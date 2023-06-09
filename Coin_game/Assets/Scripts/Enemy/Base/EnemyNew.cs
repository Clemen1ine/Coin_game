using UnityEngine;

public class EnemyNew : MonoBehaviour, IDemageable, IEnemyMoveable  
{    
    [field: SerializeField] public float MaxHealth { get; set; } = 2f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D rb { get; set; }
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= damageAmount)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void MoveEnemy(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
}
