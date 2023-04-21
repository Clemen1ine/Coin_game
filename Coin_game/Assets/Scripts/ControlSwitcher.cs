using UnityEngine;

public class ControlSwitcher : MonoBehaviour
{
    public PlayerController RB2Movement;
    public MouseMovement mousemovement;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            RB2Movement.enabled = true;
            mousemovement.enabled = false;
        }
        
        if (Input.GetMouseButton(1))
        {
            RB2Movement.enabled = false;
            mousemovement.enabled = true;
        }
    }
}