using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class shadow : MonoBehaviour
{
    [SerializeField] private string myId;

    private void OnTriggerEnter(Collider col)
    {
        peace p = col.GetComponent<peace>();
        if(p != null)
        {
            if(p.GetId() == myId)
            {
                Destroy(p.transform.GetComponent<LeanDragTranslate>());
                p.transform.position = transform.position;
                p.mCompleted = true;
                PeaceGameLevelManger.instance.checkVectory();
            }
        }
    }
}
