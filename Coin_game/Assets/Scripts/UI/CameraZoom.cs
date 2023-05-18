using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Vector3 _startTouch;
    public float maxZoom;
    public float minZoom;
    public float sensitivity;

    public void Update()
    {
        ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));

    }

    void ZoomCamera(float increment)
    {
        GetComponent<Camera>().orthographicSize = Math.Clamp(GetComponent<Camera>().orthographicSize - increment * sensitivity, minZoom, maxZoom);
    }
}