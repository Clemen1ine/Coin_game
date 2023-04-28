using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMune : MonoBehaviour
{
    public bool PauseGame;
    public GameObject PauseGameManu;
    public GameObject MiniMap;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseGameManu.SetActive(false);
        MiniMap.SetActive(true);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause()
    {
        PauseGameManu.SetActive(true);
        MiniMap.SetActive(false);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
