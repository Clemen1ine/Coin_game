using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Text countText;
    private Camera _mainCamera;
    private RectTransform _rectTransform;
    private Vector3 _offset;

    [HideInInspector]
    public Transform parentAfterDrag;
    [HideInInspector]
    public int count = 1;
    [HideInInspector]
    public Item item;

    private bool isCtrlPressed;
    private int selectedQuantity;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        _offset = _rectTransform.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Disable the count text during drag to avoid blocking interactions

        // Check if the Ctrl key is pressed
        isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        // Set the selected quantity to the current item count
        selectedQuantity = isCtrlPressed ? 1 : count;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + _offset;
        newPosition.z = _rectTransform.position.z; // Retain the original z position
        _rectTransform.position = newPosition;

        // Check for mouse wheel input
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the maximum allowed selected quantity
        int maxQuantity = isCtrlPressed ? 1 : count;

        // Increase or decrease the selected quantity based on mouse wheel input
        if (scrollDelta > 0f)
        {
            selectedQuantity = Mathf.Min(selectedQuantity + 1, maxQuantity);
        }
        else if (scrollDelta < 0f)
        {
            selectedQuantity = Mathf.Max(selectedQuantity - 1, 1);
        }

        // Update the count text to display the selected quantity
        countText.text = selectedQuantity.ToString();
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

        // Re-enable the count text after the drag ends
        countText.gameObject.SetActive(true);

        // Check if the item was dropped on a chest slot
        ChestSlot chestSlot = eventData.pointerEnter?.GetComponent<ChestSlot>();
        if (chestSlot != null)
        {
            Debug.Log("Item dropped onto ChestSlot");
            chestSlot.OnDrop(eventData);

            // Transfer the selected quantity based on the Ctrl key press
            int transferCount = isCtrlPressed ? Mathf.Min(selectedQuantity, count) : selectedQuantity;

            // Reduce the item count in the inventory item
            count -= (isCtrlPressed ? 1 : transferCount);
            RefreshCount();

            // Add the transferred items to the chest
            InventoryManager.inventoryManager.AddItemToInventory(item, transferCount);

            if (count == 0)
            {
                // The entire stack has been transferred, destroy the item object
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Item dropped outside of ChestSlot");
        }
    }
}
