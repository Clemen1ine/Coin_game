using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthDeath : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public void HurtPlayer(int damageTolive)
    {
        currentHealth -= damageTolive;
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}