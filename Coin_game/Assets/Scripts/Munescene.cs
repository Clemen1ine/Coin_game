using UnityEngine;

public class Munescene : MonoBehaviour
{
    private void Start()
    {
        // Clear the saved coin count
        PlayerPrefs.DeleteKey("coinCount");
    }
}