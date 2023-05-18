using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Type colorType;
    public Vector3 initialPosition;
    public Rigidbody2D rb;
    private bool droppedOnDropHandler = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = transform.position;
        rb.gravityScale = 0f;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!droppedOnDropHandler)
        {
            transform.position = initialPosition;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DropLocation>() != null)
        {
            droppedOnDropHandler = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<DropLocation>() != null)
        {
            droppedOnDropHandler = false;
        }
    }
}
