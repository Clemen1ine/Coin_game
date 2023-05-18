using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    private AddRoom _room;
    public int currentHealth;

    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        _room = GetComponentInParent<AddRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtEnemy(int damageTolive)
    {
        currentHealth -= damageTolive;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            _room.enemies.Remove(gameObject);
        }
    }
}
