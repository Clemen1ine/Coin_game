using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMune : MonoBehaviour
{
    [FormerlySerializedAs("PauseGame")] public bool pauseGame;
    [FormerlySerializedAs("PauseGameManu")] public GameObject pauseGameManu;
    [FormerlySerializedAs("MiniMap")] public GameObject miniMap;

    public GameObject deathMenu; // Reference to the death menu UI object

    void Update()
    {
        if (!deathMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
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

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
