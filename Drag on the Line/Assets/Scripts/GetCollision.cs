using UnityEngine;

public class GetCollision : MonoBehaviour
{
    private bool hasCollided = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasCollided)
            GameStats.collisionCount++;

        hasCollided = true;

        Debug.Log("COllisions : " + GameStats.collisionCount);
    }
}
