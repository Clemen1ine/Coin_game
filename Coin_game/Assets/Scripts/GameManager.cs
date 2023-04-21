using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject coinPrefab;
    public GameObject bigCoinPrefab;

    public int smallCoinScore = 1;
    public int bigCoinScore = 2;

    private int smallCoinCount = 0;
    private int bigCoinCount = 0;
    private int totalCoinValue = 0;

    public Text coinCounter;

    private bool showCounter = false;
    private float timeSinceLastPickup = 0f;

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

    private void Start()
    {
        HideCoinCounter();
        
        totalCoinValue = PlayerPrefs.GetInt("coinCount", 0);
        coinCounter.text = "Coins: " + totalCoinValue.ToString();
    }

    private void Update()
    {
        if (timeSinceLastPickup > 3f)
        {
            HideCoinCounter();
        }
        else if (showCounter)
        {
            ShowCoinCounter();
        }

        timeSinceLastPickup += Time.deltaTime;
    }

    private void HideCoinCounter()
    {
        coinCounter.gameObject.SetActive(false);
        showCounter = false;
    }

    private void ShowCoinCounter()
    {
        coinCounter.text = "Coins: " + totalCoinValue.ToString();
        coinCounter.gameObject.SetActive(true);
    }

    public void AddScore(int value)
    {
        if (value == smallCoinScore)
        {
            smallCoinCount++;
            totalCoinValue += 1;
        }
        else if (value == bigCoinScore)
        {
            bigCoinCount++;
            totalCoinValue += 2;
        }

        if (!showCounter && totalCoinValue > 0)
        {
            ShowCoinCounter();
            showCounter = true;
        }

        timeSinceLastPickup = 0f;
        
        PlayerPrefs.SetInt("coinCount", totalCoinValue);
    }
}
