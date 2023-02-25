using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject smallCoinPrefab;
    public GameObject bigCoinPrefab;
    public int maxCoins = 150;
    public float spawnRadius = 10f;

    private int smallCoinCount = 0;
    private int bigCoinCount = 0;

    private void Update()
    {
        if (smallCoinCount < maxCoins)
        {
            Vector2 spawnPos = GetRandomSpawnPosition();
            Instantiate(smallCoinPrefab, spawnPos, Quaternion.identity);
            smallCoinCount++;
        }

        if (bigCoinCount < maxCoins / 2)
        {
            Vector2 spawnPos = GetRandomSpawnPosition();
            Instantiate(bigCoinPrefab, spawnPos, Quaternion.identity);
            bigCoinCount++;
        }
    }

    // Returns a random position within the spawn radius
    private Vector2 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, spawnRadius);
        Vector2 spawnPos = new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
        return spawnPos;
    }
}