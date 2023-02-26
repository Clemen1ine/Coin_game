using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject smallCoinPrefab;
    public GameObject bigCoinPrefab;
    public int minSmallCoins = 40;
    public int minBigCoins = 40;
    public int maxCoins = 150;
    public float minDistance = 2f;
    public float spawnRange = 10f;

    private List<Vector3> spawnPositions = new List<Vector3>();

    void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        // Spawn the minimum number of coins for each type
        for (int i = 0; i < minSmallCoins; i++)
        {
            SpawnCoin(smallCoinPrefab);
        }

        for (int i = 0; i < minBigCoins; i++)
        {
            SpawnCoin(bigCoinPrefab);
        }

        // Spawn the rest of the coins randomly
        while (spawnPositions.Count < maxCoins)
        {
            // Check if there are no more available positions to spawn a coin
            if (spawnPositions.Count == maxCoins - 1)
            {
                break;
            }

            Vector3 position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0f);

            // Check if the position is too close to any other spawn position
            bool tooClose = false;
            foreach (Vector3 spawnPos in spawnPositions)
            {
                if (Vector3.Distance(position, spawnPos) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                // Randomly choose whether to spawn a small or big coin
                GameObject coinPrefab = Random.Range(0, 2) == 0 ? smallCoinPrefab : bigCoinPrefab;

                SpawnCoin(coinPrefab, position);
            }
        }
    }


    private void SpawnCoin(GameObject coinPrefab, Vector3 position = default)
    {
        // If position is not specified, randomly generate it within the spawn range
        if (position == default)
        {
            position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0f);
        }

        // Spawn the coin at the position
        Instantiate(coinPrefab, position, Quaternion.identity);

        // Add the position to the list of spawn positions
        spawnPositions.Add(position);
    }
}
