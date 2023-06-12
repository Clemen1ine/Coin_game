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

                itemCountToTransfer = Mathf.Min(itemCountToTransfer, CalculateRemainingSpace());

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
                int type2ItemCount = Mathf.FloorToInt(exceedingItemCount / 2f);
                int type1ItemCount = exceedingItemCount - (type2ItemCount * 2);

                // Return exceeding type 1 items to inventory
                if (type1ItemCount > 0)
                {
                    type1ItemCount = Mathf.Min(type1ItemCount, CalculateRemainingSpace());
                    InventoryManager.inventoryManager.AddItemToInventory(inventoryItem.item, type1ItemCount);
                    inventoryItem.count -= type1ItemCount;
                    inventoryItem.RefreshCount();
                }

                // Return exceeding type 2 items to inventory
                if (type2ItemCount > 0)
                {
                    type2ItemCount = Mathf.Min(type2ItemCount, CalculateRemainingSpace());
                    InventoryManager.inventoryManager.AddItemToInventory(inventoryItem.item, type2ItemCount);
                    inventoryItem.count -= type2ItemCount;
                    inventoryItem.RefreshCount();
                }

                if (inventoryItem.count == 0)
                {
                    // The entire stack has been transferred, destroy the item object
                    Destroy(inventoryItem.gameObject);
                }
            }
        }
    }

    private int CalculateRemainingSpace()
    {
        int remainingSpace = 0;
        Chest chest = transform.GetComponentInParent<Chest>();

        int itemCount = chest.GetItemCount();
        int exceedingItemCount = itemCount - chest.maxItems;
        int type2ItemCount = Mathf.FloorToInt(exceedingItemCount / 2f);
        int type1ItemCount = exceedingItemCount - (type2ItemCount * 2);

        remainingSpace = chest.maxItems - itemCount + type1ItemCount + (type2ItemCount * 2);

        return remainingSpace;
    }
}
