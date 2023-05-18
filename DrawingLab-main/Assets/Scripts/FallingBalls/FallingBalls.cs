using UnityEngine;
using UnityEngine.UI;

public class FallingBalls : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject spawnObj;

    Vector3 spawnLocation;



    private void Awake()
    {
        spawnLocation = spawnObj.transform.position - transform.position;
        Debug.Log("Position : " + spawnObj.transform.position);
        Debug.Log("spawn Loc : " + spawnLocation);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject x = Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
            x.GetComponent<RectTransform>().anchoredPosition3D = spawnLocation;
            x.GetComponent<Image>().color = Random.ColorHSV();
        }
    }
}
