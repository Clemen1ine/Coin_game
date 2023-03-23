using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject smallCoinPrefab;
    public GameObject bigCoinPrefab;
    public int maxTotalCoins = 150;
    public int maxSmallCoins = 100;
    public int maxBigCoins = 50;
    public int coinsPerRoom = 5;

    private List<Vector3> spawnPositions = new List<Vector3>();

    private GameObject mainRoom;

    void Start()
    {
        float delay = 4f; 
        Invoke("SpawnCoins", delay);
        
        mainRoom = GameObject.FindGameObjectWithTag("mainRoom");
    }

   private void SpawnCoins()
{
    spawnPositions.Clear();
    
    SpawnCoinsOfType(smallCoinPrefab, maxSmallCoins);
    
    SpawnCoinsOfType(bigCoinPrefab, maxBigCoins);
    
    int numRandomCoins = Mathf.Max(0, maxTotalCoins - maxSmallCoins - maxBigCoins);
    
    GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
    
    foreach (GameObject room in rooms)
    {
        if (room == mainRoom) continue; 
        
        Bounds roomBounds = new Bounds(room.transform.position, new Vector3(room.transform.localScale.x, room.transform.localScale.y, 0));
        
        int coinsInRoom = Mathf.Min(coinsPerRoom, maxTotalCoins - spawnPositions.Count);
        for (int i = 0; i < coinsInRoom; i++)
        {
            float x = Random.Range(roomBounds.min.x, roomBounds.max.x);
            float y = Random.Range(roomBounds.min.y, roomBounds.max.y);
            
            if (numRandomCoins > 0)
            {
                GameObject coinPrefab = Random.Range(0, 2) == 0 ? smallCoinPrefab : bigCoinPrefab;
                
                SpawnCoin(coinPrefab, roomBounds, new Vector3(x, y, 0f));

                numRandomCoins--;
            }
            else
            {
                GameObject coinPrefab = Random.Range(0, 2) == 0 ? smallCoinPrefab : bigCoinPrefab;
                
                SpawnCoin(coinPrefab, roomBounds, new Vector3(x, y, 0f));
            }
        }
    }
}



   private void SpawnCoinsOfType(GameObject coinPrefab, int maxCoins)
    {
        int numCoins = Mathf.Min(maxCoins, maxTotalCoins - spawnPositions.Count);
        for (int i = 0; i < numCoins; i++)
        {
            SpawnCoin(coinPrefab);
        }
    }

    private void SpawnCoin(GameObject coinPrefab, Bounds roomBounds = default, Vector3? position = null)
    {
        if (!position.HasValue && roomBounds != default)
        {
            position = new Vector3(Random.Range(roomBounds.min.x, roomBounds.max.x), Random.Range(roomBounds.min.y, roomBounds.max.y), 0f);
        }
        
        if (position.HasValue && spawnPositions.Count < maxTotalCoins)
        {
            Instantiate(coinPrefab, position.Value, Quaternion.identity);
            
            spawnPositions.Add(position.Value);
        }
    }
}