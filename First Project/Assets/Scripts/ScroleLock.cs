using UnityEngine;
using UnityEngine.UI;

public class ScroleLock : MonoBehaviour
{
    Scrollbar bar;

    void Start()
    {
        bar = GetComponent<Scrollbar>();
    }

    void Update()
    {
        float yVelocity = 0;
        bar.value = Mathf.SmoothDamp(bar.value,0, ref yVelocity, 10 * Time.deltaTime);
    }

}
