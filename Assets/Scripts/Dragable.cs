using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{

	void DragMe(Vector2 pos)
    {
        this.transform.position = pos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragMe(eventData.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragMe(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragMe(eventData.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Point at " + eventData.position);
    }
}
