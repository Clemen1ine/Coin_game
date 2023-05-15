using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryitem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryitem.parentAfterDrag = transform;
        }
    }
}