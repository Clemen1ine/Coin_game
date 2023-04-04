using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changeroom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;

    private Camera cam;

    void Start()
    {
        // get the Camera component of the main camera
        cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            // check if the Camera component exists
            if (cam != null)
            {
                // move the camera and player to their new positions
                other.transform.position += playerChangePos;
                cam.transform.position += cameraChangePos;
            }
            else
            {
                Debug.LogWarning("Main camera not found!");
            }
        }
    }
}