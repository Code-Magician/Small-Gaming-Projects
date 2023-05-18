using UnityEngine;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject car;
    private float carSpeed = 5;
    private bool isPressed = false;
    private int direction = 0;



    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        Debug.Log("Entered");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Debug.Log("Exited");
    }


    void Update()
    {
        if (GameProps.canPlay)
        {
            if (isPressed)
                car.transform.Translate(new Vector2(0, direction) * carSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
                car.transform.Translate(new Vector2(0, 1) * carSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.E))
                car.transform.Translate(new Vector2(0, -1) * carSpeed * Time.deltaTime);
        }
    }

    public void MoveCar(int direction)
    {
        this.direction = direction;
    }
}
