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

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Item item;

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
        countText.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + _offset;
        newPosition.z = _rectTransform.position.z; // Retain the original z position
        _rectTransform.position = newPosition;
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
        }
        else
        {
            Debug.Log("Item dropped outside of ChestSlot");
        }
    }
}