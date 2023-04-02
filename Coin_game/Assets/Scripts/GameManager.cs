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
        // Проверка на наличие другого экземпляра GameManager
        // Если другого экземпляра нет, то этот экземпляр становится единственным доступным через переменную instance
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
        // Начальное состояние: скрыть счетчик
        HideCoinCounter();
    }

    private void Update()
    {
        // Если прошло более 3 секунд после последнего сбора монет, скрыват счетчик 
        if (timeSinceLastPickup > 3f)
        {
            HideCoinCounter();
        }
        // Если showCounter=true, показівает счетчик монет
        else if (showCounter)
        {
            ShowCoinCounter();
        }

        // Обновляет время с последнего сбора монет
        timeSinceLastPickup += Time.deltaTime;
    }

    private void HideCoinCounter()
    {
        // Скрыть счетчик монет 
        coinCounter.gameObject.SetActive(false);
        showCounter = false;
    }

    private void ShowCoinCounter()
    {
        // Показать количество монет в счетчике
        coinCounter.text = "Coins: " + totalCoinValue.ToString();
        coinCounter.gameObject.SetActive(true);
    }

    public void AddScore(int value)
    {
        // Обработать сбор монеты: увеличить количество монет определенного типа и общее количество монет
        // а также обновить время с последнего сбора монет
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

        // Если счетчик монет скрыт и общее количество монет больше 0, показать счетчик 
        if (!showCounter && totalCoinValue > 0)
        {
            ShowCoinCounter();
            showCounter = true;
        }

        // Обновить время с последнего сбора монет
        timeSinceLastPickup = 0f;
    }
}