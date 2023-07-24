using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject coinPrefab;
    public GameObject bigCoinPrefab;

    public int smallCoinScore = 1;
    public int bigCoinScore = 2;

    private int _smallCoinCount = 0;
    private int _bigCoinCount = 0;
    private int _totalCoinValue = 0;

    public Text coinCounter;

    private bool _showCounter = false;
    private float _timeSinceLastPickup = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        HideCoinCounter();
        
        _totalCoinValue = PlayerPrefs.GetInt("coinCount", 0);
        coinCounter.text = _totalCoinValue.ToString();
    }

    private void Update()
    {
        if (_timeSinceLastPickup > 3f)
        {
            HideCoinCounter();
        }
        else if (_showCounter)
        {
            ShowCoinCounter();
        }

        _timeSinceLastPickup += Time.deltaTime;
    }

    private void HideCoinCounter()
    {
        coinCounter.gameObject.SetActive(false);
        _showCounter = false;
    }

    private void ShowCoinCounter()
    {
        coinCounter.text = _totalCoinValue.ToString();
        coinCounter.gameObject.SetActive(true);
    }

    public void AddScore(int value)
    {
        if (value == smallCoinScore)
        {
            _smallCoinCount++;
            _totalCoinValue += 1;
        }
        else if (value == bigCoinScore)
        {
            _bigCoinCount++;
            _totalCoinValue += 2;
        }

        if (!_showCounter && _totalCoinValue > 0)
        {
            ShowCoinCounter();
            _showCounter = true;
        }

        _timeSinceLastPickup = 0f;
        
        PlayerPrefs.SetInt("coinCount", _totalCoinValue);
    }
}
