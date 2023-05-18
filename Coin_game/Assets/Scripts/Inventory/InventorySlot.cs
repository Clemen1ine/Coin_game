using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
   public void OnDrop(PointerEventData eventData)
{
    InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

    // Check if the item is being dropped on the same slot
    if (inventoryItem.parentAfterDrag == transform)
        return;

    if (transform.childCount > 0)
    {
        InventoryItem existingItem = transform.GetChild(0).GetComponent<InventoryItem>();

        // Check if the items can stack
        if (existingItem.item == inventoryItem.item && existingItem.count < InventoryManager.inventoryManager.maxStackedItems)
        {
            int spaceAvailable = InventoryManager.inventoryManager.maxStackedItems - existingItem.count;
            int itemCountToTransfer = Mathf.Min(inventoryItem.count, spaceAvailable);

            existingItem.count += itemCountToTransfer;
            existingItem.RefreshCount();

            inventoryItem.count -= itemCountToTransfer;
            inventoryItem.RefreshCount();

            if (inventoryItem.count == 0)
            {
                // The entire stack has been transferred, destroy the item object
                Destroy(inventoryItem.gameObject);
            }
        }
        else
        {
            // Slot already has an item, swap positions
            Transform currentItem = transform.GetChild(0);
            currentItem.SetParent(inventoryItem.parentAfterDrag);
            currentItem.position = inventoryItem.parentAfterDrag.position;
        }
    }
    else
    {
        InventorySlot sourceSlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();

        // Move the entire stack to an empty slot
        inventoryItem.transform.SetParent(transform);
        inventoryItem.transform.position = transform.position;
        inventoryItem.parentAfterDrag = transform;

        // Swap items within the source slot to fill the empty space
        if (sourceSlot != null && sourceSlot.transform != transform)
        {
            InventoryItem[] itemsInSourceSlot = sourceSlot.GetComponentsInChildren<InventoryItem>();
            foreach (InventoryItem item in itemsInSourceSlot)
            {
                if (item != inventoryItem && item.parentAfterDrag == transform)
                {
                    item.parentAfterDrag = sourceSlot.transform;
                    item.transform.SetParent(sourceSlot.transform);
                    item.transform.position = sourceSlot.transform.position;
                }
            }
        }
    }
}
}
