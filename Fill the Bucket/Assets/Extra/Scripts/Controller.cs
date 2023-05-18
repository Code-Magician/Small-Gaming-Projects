using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject bucketPrefab;

    private void Awake()
    {
        Instantiate(bucketPrefab, Vector3.zero, Quaternion.identity, this.transform).transform.localPosition = new Vector3(Random.Range(-285, 285), -625, 0);
    }
}
