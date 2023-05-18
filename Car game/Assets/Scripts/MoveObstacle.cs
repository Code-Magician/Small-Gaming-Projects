using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    void Update()
    {
        if (GameProps.canPlay)
            transform.Translate(Vector2.left * 10 * Time.deltaTime);
    }
}
