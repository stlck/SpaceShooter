using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler
{
	public Transform target;

	void DragMe(Vector2 pos)
    {
		if (target == null)
						this.transform.position = pos;
				else
						target.position = pos;
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
        DragMe(eventData.position);
        //Debug.Log("Point at " + eventData.position);
    }


	public void OnPointerDown (PointerEventData eventData)
	{
        DragMe(eventData.position);
	}
}
