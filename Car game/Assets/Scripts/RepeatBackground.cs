using UnityEngine;
using System.Collections;

public class RepeatBackground : MonoBehaviour
{
    public bool gameStarted;
    [SerializeField] GameObject bg1;
    [SerializeField] GameObject bg2;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject finishLineObj;
    public GameObject finishLine;

    public float halfWidth;
    private Vector2 bg1_startPos;
    private Vector2 bg2_startPos;

    public int bgSpeed = 20;


    public float screenHeight;
    public float screenWidth;
    public float screenLeft;
    public float screenRight;
    public float screenTop;
    public float screenBottom;

    private float spawnX;

    public int hitCount = 0;

    private int InitialLevelObstacleCount = 5;
    private int increaseLevelObstacleCount = 2;
    public int currentLevelObstacleCount = 0;



    private void Awake()
    {
        CalculateScreenEdgePostions();
    }
    void Start()
    {
        bg1_startPos = bg1.transform.position;
        bg2_startPos = bg2.transform.position;

        halfWidth = bg2.GetComponent<BoxCollider2D>().size.x * bg2.transform.localScale.x;
        StartCoroutine(Spawn());
    }

    void Update()
    {
        if (GameProps.canPlay)
        {
            if (bg1.transform.position.x <= (bg1_startPos.x - halfWidth))
            {
                bg1.transform.position = bg1_startPos;
                bg2.transform.position = bg2_startPos;
            }
            else
            {
                bg1.transform.Translate(Vector2.left * bgSpeed * Time.deltaTime);
                bg2.transform.Translate(Vector2.left * bgSpeed * Time.deltaTime);
            }
        }

        CheckScreenSizeChanged();
    }



    private void CalculateScreenEdgePostions()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);

        lowerLeftCornerScreen = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        upperRightCornerScreen = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        screenLeft = lowerLeftCornerScreen.x;
        screenBottom = lowerLeftCornerScreen.y;
        screenTop = upperRightCornerScreen.y;
        screenRight = upperRightCornerScreen.x;

        spawnX = screenRight + halfWidth;
    }

    private void CheckScreenSizeChanged()
    {
        if (screenWidth != Screen.width ||
            screenHeight != Screen.height)
        {
            CalculateScreenEdgePostions();
        }
    }


    private IEnumerator Spawn()
    {
        while (true)
        {
            if (GameProps.canPlay)
            {
                Vector2 spwanPos = new Vector2(spawnX, Random.Range(screenBottom, screenTop));

                if (GameProps.isLevelGame) currentLevelObstacleCount++;
                if (currentLevelObstacleCount < InitialLevelObstacleCount + GameProps.currentLevel * increaseLevelObstacleCount)
                    Instantiate(obstaclePrefab, spwanPos, Quaternion.identity);
                else
                    finishLine = Instantiate(finishLineObj, new Vector2(spawnX, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }

}
