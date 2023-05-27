using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 pointerOffset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pointerOffset);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Vector2 pointerPosition = ClampToWindow(eventData);
            rectTransform.localPosition = pointerPosition - pointerOffset;
        }
    }

    private Vector2 ClampToWindow(PointerEventData eventData)
    {
        Vector2 rawPointerPosition = eventData.position;
        Vector3[] canvasCorners = new Vector3[4];
        rectTransform.parent.GetComponent<RectTransform>().GetWorldCorners(canvasCorners);

        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        return new Vector2(clampedX, clampedY);
    }
}