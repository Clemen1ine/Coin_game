using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public QuantitySelectionUI quantitySelectionUI; // Reference to the QuantitySelectionUI script

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

                // Check if the Ctrl key is held down
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    quantitySelectionUI.OpenQuantitySelection();

                    // Wait for the player to confirm the selected quantity
                    // You can use an event system or a button click to handle this interaction
                    // Once the player confirms, retrieve the selected quantity from the quantitySelectionUI script
                    // Let's assume the selected quantity is retrieved through the GetSelectedQuantity() method

                    // Clamp the selected quantity within the valid range
                    itemCountToTransfer = Mathf.Clamp(quantitySelectionUI.GetSelectedQuantity(), 1, itemCountToTransfer);

                    quantitySelectionUI.CloseQuantitySelection();
                }

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
            // Move the entire stack to an empty slot
            InventorySlot sourceSlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();

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
