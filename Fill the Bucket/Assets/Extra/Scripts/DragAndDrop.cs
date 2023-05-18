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
        transform.position = new Vector3(transform.position.x + ((Vector3)eventData.delta).x, transform.position.y, transform.position.z);
    }



    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
