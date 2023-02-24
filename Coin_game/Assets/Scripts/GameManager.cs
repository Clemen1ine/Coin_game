using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject coinPrefab;
    public GameObject bigCoinPrefab;


    public int smallCoinScore = 1;
    public int bigCoinScore = 2;

    private int smallCoinCount = 0;
    private int bigCoinCount = 0;

    public Text smallCoinCounter;
    public Text bigCoinCounter;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        if (value == smallCoinScore)
        {
            smallCoinCount++;
            smallCoinCounter.text = "Small Coins: " + smallCoinCount.ToString();
            Debug.Log("Small coin collected! Count: " + smallCoinCount);
        }
        else if (value == bigCoinScore)
        {
            bigCoinCount++;
            bigCoinCounter.text = "Big Coins: " + bigCoinCount.ToString();
            Debug.Log("Big coin collected! Count: " + bigCoinCount);
        }
    }
}

