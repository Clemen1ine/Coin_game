using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public GameObject chestUI;
    public int maxItems = 10; // Maximum number of items in the chest
    private bool isPlayerInRange;
    private bool isChestOpen;
    private ChestSlot[] chestSlots; // Array of ChestSlot components representing the chest slots

    private void Start()
    {
        // Initialize the chestSlots array with ChestSlot components from the children of the chestUI object
        chestSlots = chestUI.GetComponentsInChildren<ChestSlot>();

        // Debug the chestSlots array
        Debug.Log("ChestSlots Count: " + chestSlots.Length);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            CloseChest();
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            if (isChestOpen)
            {
                CloseChest();
            }
            else
            {
                OpenChest();
            }
        }
    }

    private void OpenChest()
    {
        // Check the item count before opening the chest
        int itemCount = GetItemCount();
        if (itemCount > maxItems)
        {
            Debug.Log("Cannot open the chest. Maximum item limit reached.");
            return;
        }

        chestUI.SetActive(true);
        isChestOpen = true;

        // Update the item count when the chest is opened
        RefreshItemCount();
    }

    private void CloseChest()
    {
        chestUI.SetActive(false);
        isChestOpen = false;
    }

    public int GetItemCount()
    {
        int itemCount = 0;

        foreach (ChestSlot slot in chestSlots)
        {
            // Count the initial items in the chest
            InventoryItem[] initialItems = slot.GetComponentsInChildren<InventoryItem>(false);
            foreach (InventoryItem item in initialItems)
            {
                Text textComponent = item.countText;
                if (textComponent != null && int.TryParse(textComponent.text, out int value))
                {
                    itemCount += value;
                }
            }
        }

        Debug.Log("Item count: " + itemCount);
        return itemCount;
    }

    public void RefreshItemCount()
    {
        int itemCount = GetItemCount();
        // Do something with the item count, such as displaying it in the UI
        Debug.Log("Item count: " + itemCount);
    }
}