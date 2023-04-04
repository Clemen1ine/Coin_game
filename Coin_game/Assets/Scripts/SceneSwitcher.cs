using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public Button backButton;
    
    void Start()
    {
        // Add a listener to the backButton
        backButton.onClick.AddListener(ReturnToPreviousScene);
    }
    
    public void SwitchToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    
    public void ReturnToPreviousScene()
    {
        // Get the index of the previous scene
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        // Load the previous scene if it exists, otherwise load the first scene
        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

}