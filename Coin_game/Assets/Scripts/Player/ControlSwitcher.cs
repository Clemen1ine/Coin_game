using UnityEngine;
using UnityEngine.Serialization;

public class ControlSwitcher : MonoBehaviour
{
    [FormerlySerializedAs("RB2Movement")] public PlayerController rb2Movement;
    public MouseMovement mousemovement;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rb2Movement.enabled = true;
            mousemovement.enabled = false;
        }
        
        if (Input.GetMouseButton(1))
        {
            rb2Movement.enabled = false;
            mousemovement.enabled = true;
        }
    }
}