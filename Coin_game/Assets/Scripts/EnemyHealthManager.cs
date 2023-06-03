using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    private AddRoom _room;
    public int currentHealth;
    public int maxHealth;
    public GameObject dropObjectPrefab;
    public float dropChance;

    void Start()
    {
        _room = GetComponentInParent<AddRoom>();
    }

    public void HurtEnemy(int damageToLive)
    {
        currentHealth -= damageToLive;
        if (currentHealth <= 0)
        {
            if (Random.value <= dropChance)
            {
                DropObject();
            }

            Destroy(gameObject);
            _room.enemies.Remove(gameObject);
        }
    }

    private void DropObject()
    {
        if (dropObjectPrefab != null)
        {
            Instantiate(dropObjectPrefab, transform.position, Quaternion.identity);
        }
    }
}
