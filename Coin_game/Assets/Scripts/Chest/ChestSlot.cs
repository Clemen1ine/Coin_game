using UnityEngine;
using UnityEngine.EventSystems;

public class ChestSlot : MonoBehaviour, IDropHandler
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
        if (existingItem.item == inventoryItem.item &&
            existingItem.count < InventoryManager.inventoryManager.maxStackedItems)
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
            Transform originalParent = inventoryItem.parentAfterDrag;

            currentItem.SetParent(originalParent);
            currentItem.position = originalParent.position;

            inventoryItem.transform.SetParent(transform);
            inventoryItem.transform.position = transform.position;
            inventoryItem.parentAfterDrag = transform;
        }
    }
    else
    {
        ChestSlot chestSlot = inventoryItem.parentAfterDrag.GetComponent<ChestSlot>();

        // Move the entire stack to an empty slot
        inventoryItem.transform.SetParent(transform);
        inventoryItem.transform.position = transform.position;
        inventoryItem.parentAfterDrag = transform;

        // Swap items within the source slot to fill the empty space
        if (chestSlot != null && chestSlot.transform != transform)
        {
            InventoryItem[] itemsInSourceSlot = chestSlot.GetComponentsInChildren<InventoryItem>();
            foreach (InventoryItem item in itemsInSourceSlot)
            {
                if (item != inventoryItem && item.parentAfterDrag == transform)
                {
                    item.parentAfterDrag = chestSlot.transform;
                    item.transform.SetParent(chestSlot.transform);
                    item.transform.position = chestSlot.transform.position;
                }
            }
        }

        // Check if the chest is full and return the exceeding items to the inventory
        Chest chest = transform.GetComponentInParent<Chest>();
        chest.RefreshItemCount();

        if (chest.GetItemCount() > chest.maxItems)
        {
            int exceedingItemCount = chest.GetItemCount() - chest.maxItems;
            InventoryManager.inventoryManager.AddItemToInventory(inventoryItem.item, exceedingItemCount);
            inventoryItem.count -= exceedingItemCount;
            inventoryItem.RefreshCount();

            if (inventoryItem.count == 0)
            {
                // The entire stack has been transferred, destroy the item object
                Destroy(inventoryItem.gameObject);
            }
        }
    }
}

}
