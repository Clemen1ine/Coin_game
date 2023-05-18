using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int smallCoinValue = 1; 
    public int bigCoinValue = 2; 
    public float bigCoinRadius = 2.0f; 
    public Item item;
    
    private List<GameObject> _bigCoinsInRange = new List<GameObject>(); 
    private GameObject _hintObject;
    

    private void Start()
    {
        try
        {
            _hintObject = transform.GetChild(0).gameObject;
            _hintObject.SetActive(false);
        }
        catch (UnityException e)
        {
            Debug.Log(e);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.tag == "SmallCoin")
            {
                GameManager.Instance.AddScore(smallCoinValue);
                Destroy(gameObject);
                InventoryManager.inventoryManager.AddItemToInventory(item);
            }
            else if (gameObject.tag == "BigCoin")
            {
                _bigCoinsInRange.Add(gameObject);
                _hintObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.tag == "BigCoin")
            {
                _bigCoinsInRange.Remove(gameObject);
                _hintObject.SetActive(false);
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

        foreach (GameObject coin in _bigCoinsInRange)
        {
            if (Vector2.Distance(transform.position, coin.transform.position) <= bigCoinRadius)
            {
                coinsToDestroy.Add(coin);
                GameManager.Instance.AddScore(bigCoinValue);
            }
        }

        foreach (GameObject coin in coinsToDestroy)
        {
            _bigCoinsInRange.Remove(coin);
            Destroy(coin);
            InventoryManager.inventoryManager.AddItemToInventory(item);
        }
        _hintObject.SetActive(false);
    }

}
