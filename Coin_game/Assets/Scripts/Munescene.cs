using UnityEngine;

public class Munescene : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteKey("coinCount");
    }
}