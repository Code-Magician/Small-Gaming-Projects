using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static int timesDropped = 0;
    [SerializeField] new GameObject particleSystem;
    [SerializeField] GameObject mugIndicator;

    Animator animationController;
    private Vector3 initialPosition;



    private void Awake()
    {
        animationController = GetComponent<Animator>();
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        particleSystem.SetActive(false);
        mugIndicator.SetActive(true);

        animationController.SetTrigger("Idle");

        initialPosition = transform.position;
    }



    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }



    public void OnEndDrag(PointerEventData eventData)
    {

        Vector3 canvasRectPos = this.transform.parent.transform.position;
        Vector3 mugRectPos = this.transform.position;
        Vector3 mugPosWrtCanvas = mugRectPos - canvasRectPos;

        bool horizontalConstraints = mugPosWrtCanvas.x >= 0 && mugPosWrtCanvas.x <= 300;
        bool verticalConstraints = mugPosWrtCanvas.y >= -200 && mugPosWrtCanvas.y <= 500;

        if (horizontalConstraints && verticalConstraints)
        {
            animationController.SetBool("Tilt", true);
            mugIndicator.SetActive(false);


            timesDropped++;
            Debug.Log(timesDropped);
        }
    }



    public void RunDropAnimation()
    {
        animationController.ResetTrigger("Idle");
        animationController.SetBool("Tilt", false);
        particleSystem.SetActive(true);
    }
}
