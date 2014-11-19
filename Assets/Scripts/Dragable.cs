using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
}
