using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changeroom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    public float cameraSpeed = 5f;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_cam != null)
            {
                StartCoroutine(MoveCameraSmoothly(other.transform));
            }
            else
            {
                Debug.LogWarning("Main camera not found!");
            }
        }
    }

    private IEnumerator MoveCameraSmoothly(Transform player)
    {
        player.transform.position += playerChangePos;
        
        Vector3 initialCameraPos = _cam.transform.position;
        Vector3 targetCameraPos = initialCameraPos + cameraChangePos;
        
        while (_cam.transform.position != targetCameraPos)
        {
            Vector3 newCameraPos = Vector3.MoveTowards(_cam.transform.position, targetCameraPos, cameraSpeed * Time.deltaTime);
            
            _cam.transform.position = newCameraPos;
            
            yield return null;
        }
    }
}