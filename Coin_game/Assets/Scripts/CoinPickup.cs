using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int smallCoinValue = 1; 
    public int bigCoinValue = 2; 
    public float bigCoinRadius = 2.0f; 

    private bool canPickupBigCoin = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            canPickupBigCoin = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.tag == "SmallCoin")
            {
                GameManager.instance.AddScore(smallCoinValue);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (canPickupBigCoin && gameObject.tag == "BigCoin" && Vector2.Distance(transform.position, other.transform.position) <= bigCoinRadius)
            {
                GameManager.instance.AddScore(bigCoinValue);
                Destroy(gameObject);
            }
        }
    }
}
