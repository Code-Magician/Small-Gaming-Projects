using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] GameObject obstaclePrefab;
    float[] positionsX = { -0.35f, 0, 0.35f };
    float offsetZ = 0.1f, maxZ = 0.5f, startZ = -0.4f;


    private void Start()
    {
        SpawnObstacles();
    }


    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z <= -100) Destroy(this.gameObject);
    }


    private void SpawnObstacles()
    {
        float stZ = startZ;

        while (stZ <= maxZ)
        {
            GameObject obstacleObj = Instantiate(obstaclePrefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);

            obstacleObj.name = "Obstacle";

            float xPos = positionsX[Random.Range(0, 3)];
            obstacleObj.transform.localPosition = new Vector3(xPos, 1, stZ);

            stZ += offsetZ;
        }
    }
}
