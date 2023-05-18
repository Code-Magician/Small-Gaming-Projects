using UnityEngine;
using mobilePhone;
using Lean.Touch;

public class tab : MonoBehaviour
{
    public Camera cam;

    private LeanDragTranslate drag;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<button>())
                {
                    hit.collider.GetComponent<button>().act();
                }

                if (hit.collider.GetComponent<LeanDragTranslate>())
                {
                    drag = hit.collider.GetComponent<LeanDragTranslate>();
                    drag.enabled = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (drag != null)
            {
                drag.enabled = false;
            }
        }

    }
}
