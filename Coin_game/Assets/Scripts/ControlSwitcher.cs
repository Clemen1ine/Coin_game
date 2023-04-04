using UnityEngine;

public class ControlSwitcher : MonoBehaviour
{
    public PlayerController playerController;
    public MouseController mouseController;

    void Update()
    {
        // Check for keyboard input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // Enable PlayerController and disable MouseController
            playerController.enabled = true;
            mouseController.enabled = false;
        }

        // Check for mouse input
        if (Input.GetMouseButton(1))
        {
            // Enable MouseController and disable PlayerController
            playerController.enabled = false;
            mouseController.enabled = true;
        }
    }
}
