using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public GameObject chestUI;
    public int maxItems = 10; // Maximum number of items in the chest
    public ChestSlot[] chestSlots; // Array of ChestSlot components representing the chest slots
    private bool _isPlayerInRange;
    private bool _isChestOpen;

    private int itemCount; // Total item count in the chest

    private void Start()
    {
        // Initialize the chestSlots array with ChestSlot components from the children of the chestUI object
        chestSlots = chestUI.GetComponentsInChildren<ChestSlot>();

        // Debug the chestSlots array
        Debug.Log("ChestSlots Count: " + chestSlots.Length);
        
        // Count the initial items in the chest
        RefreshItemCount();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            CloseChest();
        }
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            if (_isChestOpen)
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
        chestUI.SetActive(true);
        _isChestOpen = true;

        RefreshItemCount();
    }

    private void CloseChest()
    {
        chestUI.SetActive(false);
        _isChestOpen = false;
    }

    public int GetItemCount()
    {
        int count = 0;

        foreach (ChestSlot slot in chestSlots)
        {
            InventoryItem[] items = slot.GetComponentsInChildren<InventoryItem>(false);
            foreach (InventoryItem item in items)
            {
                Text textComponent = item.countText;
                if (textComponent != null && int.TryParse(textComponent.text, out int value))
                {
                    count += value;
                }
            }
        }

        return count;
    }

    public void RefreshItemCount()
    {
        itemCount = GetItemCount();
        // Do something with the item count, such as displaying it in the UI
        Debug.Log("Item count: " + itemCount);
    }
}
