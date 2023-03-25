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
            if (numRandomCoins > 0)
            {
                GameObject coinPrefab = Random.Range(0, 2) == 0 ? smallCoinPrefab : bigCoinPrefab;

                SpawnCoin(coinPrefab, roomBounds);

                numRandomCoins--;
            }
            else
            {
                GameObject coinPrefab = Random.Range(0, 2) == 0 ? smallCoinPrefab : bigCoinPrefab;

                SpawnCoin(coinPrefab, roomBounds);
            }
        }
    }
}

    private void SpawnCoin(GameObject coinPrefab, Bounds roomBounds = default, Vector3 position = default)
    {
        // If position is not specified, randomly generate it within the room bounds
        if (position == default && roomBounds != default)
        {
            // Generate a random point within the room bounds
            float x = Random.Range(roomBounds.min.x, roomBounds.max.x);
            float y = Random.Range(roomBounds.min.y, roomBounds.max.y);
            Vector3 randomPoint = new Vector3(x, y, 0f);

            // Generate a random offset to shift the position of the coin
            float xOffset = Random.Range(-0.5f, 0.5f) * coinPrefab.transform.localScale.x;
            float yOffset = Random.Range(-0.5f, 0.5f) * coinPrefab.transform.localScale.y;
            Vector3 offset = new Vector3(xOffset, yOffset, 0f);

            // Set the position of the coin to the random point plus the random offset
            position = randomPoint + offset;
        }

        // Spawn the coin at the position if the maximum number of coins has not been reached
        if (position != default && spawnPositions.Count < maxTotalCoins)
        {
            Instantiate(coinPrefab, position, Quaternion.identity);

            // Add the position to the list of spawn positions
            spawnPositions.Add(position);
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
}