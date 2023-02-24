using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int smallCoinValue = 1; 
    public int bigCoinValue = 2; 
    public float bigCoinRadius = 2.0f; 

    private List<GameObject> bigCoinsInRange = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.tag == "SmallCoin")
            {
                GameManager.instance.AddScore(smallCoinValue);
                Destroy(gameObject);
            }
            else if (gameObject.tag == "BigCoin")
            {
                bigCoinsInRange.Add(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.tag == "BigCoin")
            {
                bigCoinsInRange.Remove(gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpBigCoinsInRange();
        }
    }

    private void PickUpBigCoinsInRange()
    {
        List<GameObject> coinsToDestroy = new List<GameObject>();

        foreach (GameObject coin in bigCoinsInRange)
        {
            if (Vector2.Distance(transform.position, coin.transform.position) <= bigCoinRadius)
            {
                coinsToDestroy.Add(coin);
                GameManager.instance.AddScore(bigCoinValue);
            }
        }

        foreach (GameObject coin in coinsToDestroy)
        {
            bigCoinsInRange.Remove(coin);
            Destroy(coin);
        }
    }
}