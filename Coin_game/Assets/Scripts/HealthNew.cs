using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNew : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public void HurtPlayer(int damageTolive)
    {
        currentHealth -= damageTolive;
    }
}
