using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
  private float waitToload = 2f;
  private bool reloading;

  private void Start()
  {

  }

  private void Update()
  {
    if (reloading)
    {
      waitToload -= Time.deltaTime;
      if (waitToload <= 0)
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
    }
  }

  private void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      other.gameObject.GetComponent<HealthNew>().HurtPlayer(10);
      Health playerHealth = other.gameObject.GetComponent<Health>();
      playerHealth.health--;
    }
  }
}