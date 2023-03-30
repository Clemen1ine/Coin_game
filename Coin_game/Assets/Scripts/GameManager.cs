using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject coinPrefab;
    public GameObject bigCoinPrefab;

    public int smallCoinScore = 1;
    public int bigCoinScore = 2;
    public int totalCoinValue = 0;
    private int smallCoinCount = 0;
    private int bigCoinCount = 0;

    public Text coinCounter;

    private bool showCounter = false;
    private float timeSinceLastPickup = 0f;

    private void Awake()
    {
        // Check for another instance of GameManager
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
        // Load saved coin value from PlayerPrefs
        if (PlayerPrefs.HasKey("TotalCoinValue"))
        {
            totalCoinValue = PlayerPrefs.GetInt("TotalCoinValue");
        }

        // Initialize coin counter UI
        HideCoinCounter();
    }

    private void Update()
    {
        // Hide coin counter if time since last pickup > 3 seconds
        if (timeSinceLastPickup > 3f)
        {
            HideCoinCounter();
        }
        // Show coin counter if showCounter=true
        else if (showCounter)
        {
            ShowCoinCounter();
        }

        // Update time since last pickup
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
            Debug.Log("Small coin collected! Value: " + smallCoinScore);
        }
        else if (value == bigCoinScore)
        {
            bigCoinCount++;
            totalCoinValue += 2;
            Debug.Log("Big coin collected! Value: " + bigCoinScore);
        }

        if (!showCounter && totalCoinValue > 0)
        {
            ShowCoinCounter();
            showCounter = true;
        }

        timeSinceLastPickup = 0f;
    }
    
    public int GetTotalCoinValue()
    {
        return totalCoinValue;
    }

    private void OnApplicationQuit()
    {
        // Save total coin value to PlayerPrefs when the application is closed
        PlayerPrefs.SetInt("TotalCoinValue", totalCoinValue);
    }
}
