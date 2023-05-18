using UnityEngine;

public class CarCollision : MonoBehaviour
{
    [SerializeField] GameManager manager;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            GameProps.hitCount++;
            if (GameProps.hitCount >= 3)
                GameProps.canPlay = false;
            else
                Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Finish")
        {
            GameProps.canPlay = false;
            GameProps.currentLevel++;

            manager.nextLevel.SetActive(true);
        }
    }
}
