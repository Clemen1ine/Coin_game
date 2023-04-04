using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int smallCoinValue = 1; 
    public int bigCoinValue = 2; 
    public float bigCoinRadius = 2.0f; 

    private List<GameObject> bigCoinsInRange = new List<GameObject>(); // список больших монет, находящихся в радиусе обнаружения

    // выполняется при столкновении с коллайдером
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // если столкнулись с игроком
        {
            if (gameObject.tag == "SmallCoin") // если это небольшая монета
            {
                GameManager.instance.AddScore(smallCoinValue); // увеличиваем счет
                Destroy(gameObject); // удаляем монету
            }
            else if (gameObject.tag == "BigCoin") // если это большая монета
            {
                bigCoinsInRange.Add(gameObject); // добавляем ее в список монет в радиусе обнаружения
            }
        }
    }

    // выполняется при выходе из коллайдера
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // если вышли из зоны столкновения с игроком
        {
            if (gameObject.tag == "BigCoin") // если это большая монета
            {
                bigCoinsInRange.Remove(gameObject); // удаляем ее из списка монет в радиусе обнаружения
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // если была нажата клавиша "E"
        {
            PickUpBigCoinsInRange(); // подобрать монеты в радиусе обнаружения
        }
    }

    // подбирает монеты в радиусе обнаружения
    private void PickUpBigCoinsInRange()
    {
        List<GameObject> coinsToDestroy = new List<GameObject>(); // список монет для удаления

        foreach (GameObject coin in bigCoinsInRange) // список монет в радиусе обнаружения
        {
            if (Vector2.Distance(transform.position, coin.transform.position) <= bigCoinRadius) // если монета находится в радиусе обнаружения
            {
                coinsToDestroy.Add(coin); // добавляет в список монет для удаления
                GameManager.instance.AddScore(bigCoinValue); // счет++
            }
        }

        // удаляем монеты из списка монет в радиусе обнаружения
        foreach (GameObject coin in coinsToDestroy) 
        {
            bigCoinsInRange.Remove(coin);
            Destroy(coin);
        }
    }
}