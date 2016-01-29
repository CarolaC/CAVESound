using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class DragInstrument : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private GameObject draggingPanel;
    private RectTransform draggingTransform;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        var canvas = transform.parent;
        if (canvas == null)
            return;

        // create a duplicate of the panel
        draggingPanel = (GameObject)Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);

        draggingPanel.transform.SetParent(canvas.transform, false);
        draggingPanel.transform.SetAsLastSibling();

        draggingTransform = transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingPanel != null)
            SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
            draggingTransform = eventData.pointerEnter.transform as RectTransform;

        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            draggingPanel.GetComponent<RectTransform>().position = globalMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (draggingPanel != null)
            Destroy(draggingPanel);
    }
}
