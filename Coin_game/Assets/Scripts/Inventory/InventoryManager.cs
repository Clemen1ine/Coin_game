    using UnityEngine;
    using UnityEngine.Serialization;

    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager inventoryManager;
        public int maxStackedItems = 20;
        public InventorySlot[] inventorySlots;
        public GameObject inventoryItemPrefab;

        private void Awake()
        {
            inventoryManager = this;
        }

        public bool AddItemToInventory(Item item, int count = 1)
        {
            int remainingCount = count;

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
                {
                    int spaceAvailable = maxStackedItems - itemInSlot.count;
                    int itemCountToTransfer = Mathf.Min(remainingCount, spaceAvailable);

                    itemInSlot.count += itemCountToTransfer;
                    itemInSlot.RefreshCount();

                    remainingCount -= itemCountToTransfer;

                    if (remainingCount == 0)
                    {
                        // All items transferred, exit the loop
                        return true;
                    }
                }
            }

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    int itemCountToTransfer = Mathf.Min(remainingCount, maxStackedItems);

                    SpawnNewItem(item, slot, itemCountToTransfer);

                    remainingCount -= itemCountToTransfer;

                    if (remainingCount == 0)
                    {
                        // All items transferred, exit the loop
                        return true;
                    }
                }
            }

            // If execution reaches here, not all items were transferred, return false
            return false;
        }


        public void SpawnNewItem(Item item, InventorySlot slot, int count)
        {
            GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
            InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
            inventoryItem.InitialiseItem(item);
            inventoryItem.count = count;
            inventoryItem.RefreshCount();
        }
    }
