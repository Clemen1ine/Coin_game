using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Add your code for handling the beginning of the drag operation here
        Debug.Log("Drag started");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPosition = new Vector3(eventData.position.x, eventData.position.y, mainCamera.nearClipPlane);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Add your code for handling the end of the drag operation here
        Debug.Log("Drag ended");
    }
}

