using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("!");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.SetAsFirstSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("23");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("233232");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
    
    
}
