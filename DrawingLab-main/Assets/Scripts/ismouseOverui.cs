using UnityEngine;
using UnityEngine.EventSystems;

public class ismouseOverui : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public static bool ismouseover = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        ismouseover = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // ismouseover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ismouseover = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ismouseover = false;
    }

    public void Onpointerup()
    {
        ismouseover = false;
    }
}
