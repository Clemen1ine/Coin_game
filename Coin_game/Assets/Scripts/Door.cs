using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject block;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Block") && !other.CompareTag("Door"))
        {
            Transform currentTransform = transform;
            bool isInMainRoom = false;

            // Loop through all the parent objects until we reach the root of the scene
            while (currentTransform != null)
            {
                if (currentTransform.CompareTag("Room"))
                {
                    isInMainRoom = true;
                    break;
                }

                currentTransform = currentTransform.parent;
            }

            if (isInMainRoom)
            {
                Debug.Log("Door in main room, not replacing with block.");
                return;
            }

            Debug.Log("Replacing door with block...");
            Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}