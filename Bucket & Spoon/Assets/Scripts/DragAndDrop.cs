using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector3 initialPosition;


    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = transform.position;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }


    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
