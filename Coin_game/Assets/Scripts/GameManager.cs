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
        // Проверка на наличие другого экземпляра GameManager.
        // Если другого экземпляра нет, то этот экземпляр становится единственным доступным через статическую переменную instance.
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
        // Начальное состояние: скрытие счетчика монет.
        HideCoinCounter();
    }

    private void Update()
    {
        // Если прошло более 3 секунд после последнего сбора монет, скрыть счетчик монет.
        if (timeSinceLastPickup > 3f)
        {
            HideCoinCounter();
        }
        // Если showCounter=true, показать счетчик монет.
        else if (showCounter)
        {
            ShowCoinCounter();
        }

        // Обновить время с последнего сбора монет.
        timeSinceLastPickup += Time.deltaTime;
    }

    private void HideCoinCounter()
    {
        // Скрыть счетчик монет и установить showCounter=false.
        coinCounter.gameObject.SetActive(false);
        showCounter = false;
    }

    private void ShowCoinCounter()
    {
        // Показать количество монет в счетчике.
        coinCounter.text = "Coins: " + totalCoinValue.ToString();
        coinCounter.gameObject.SetActive(true);
    }

    public void AddScore(int value)
    {
        // Обработать сбор монеты: увеличить количество монет определенного типа и общее количество монет,
        // а также обновить время с последнего сбора монет.
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

        // Если счетчик монет скрыт и общее количество монет больше 0, показать счетчик монет и установить showCounter=true.
        if (!showCounter && totalCoinValue > 0)
        {
            ShowCoinCounter();
            showCounter = true;
        }

        // Обновить время с последнего сбора монет.
        timeSinceLastPickup = 0f;
    }
}
