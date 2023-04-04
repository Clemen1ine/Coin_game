using UnityEngine;

public class ControlSwitcher : MonoBehaviour
{
    public PlayerController playerController;
    public MouseController mouseController;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            playerController.enabled = true;
            mouseController.enabled = false;
        }
        
        if (Input.GetMouseButton(1))
        {
            playerController.enabled = false;
            mouseController.enabled = true;
        }
    }
}
