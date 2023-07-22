using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject ButtonPlay;
    public GameObject ButtonSettings;

    private bool _isMenuOpen = false;

    private void Start()
    {
        menuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isMenuOpen)
                CloseMenu();
        }
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        _isMenuOpen = true;
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        _isMenuOpen = false;

        ButtonPlay.SetActive(true);
        ButtonSettings.SetActive(true);
    }
}
