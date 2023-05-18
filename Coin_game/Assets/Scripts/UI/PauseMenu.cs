using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    [FormerlySerializedAs("PauseGame")] public bool pauseGame;
    [FormerlySerializedAs("PauseGameManu")] public GameObject pauseGameManu;
    [FormerlySerializedAs("MiniMap")] public GameObject miniMap;
    [FormerlySerializedAs("InventoryMenu")] public GameObject inventoryMenu;

    private bool _inventoryOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_inventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    public void Resume()
    {
        pauseGameManu.SetActive(false);
        miniMap.SetActive(true);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    public void Pause()
    {
        pauseGameManu.SetActive(true);
        miniMap.SetActive(false);
        Time.timeScale = 0f;
        pauseGame = true;
    }

    public void OpenInventory()
    {
        inventoryMenu.SetActive(true);
        _inventoryOpen = true;
    }

    public void CloseInventory()
    {
        inventoryMenu.SetActive(false);
        _inventoryOpen = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}