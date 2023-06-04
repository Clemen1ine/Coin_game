using UnityEngine;

public class CharacterSettingsManager : MonoBehaviour
{
    public GameObject resetObject;

    private void Start()
    {
        if (resetObject != null && resetObject.activeSelf)
        {
            ResetPlayerPrefs();
        }
        else
        {
            LoadCharacterSettings();
        }
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs reset!");
    }

    private void LoadCharacterSettings()
    {
        // Load and apply character settings here
        Debug.Log("Character settings loaded!");
    }
}