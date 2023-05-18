using UnityEngine;

// for adjusting right box from editor
[ExecuteInEditMode]
public class BoundingBox2d : MonoBehaviour
{
    public float colDepth = 4f, zPosition = 0f, RightColliderAdjust, leftColliderAdjust;
    public Transform topCollider, bottomCollider, leftCollider, rightCollider;
    private Vector2 screenSize;
    private Vector3 cameraPos;

    void Start()
    {
        //Generate world space point information for position and scale calculations
        cameraPos = Camera.main.transform.position;
        Vector2 origin = new Vector2(0, 0), ScreenEndPoints = new Vector2(Screen.width - RightColliderAdjust, 0);
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(origin), Camera.main.ScreenToWorldPoint(ScreenEndPoints)) * 0.5f;
        ScreenEndPoints.x = 0;
        ScreenEndPoints.y = Screen.height;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(origin), Camera.main.ScreenToWorldPoint(ScreenEndPoints)) * 0.5f;

        //Change our scale and positions to match the edges of the screen...   
        rightCollider.localScale = new Vector3(colDepth * 2, screenSize.y * 2, colDepth);
        rightCollider.position = new Vector3(cameraPos.x + screenSize.x, cameraPos.y, zPosition); //+ (rightCollider.localScale.x * 0.5f)
        leftCollider.localScale = rightCollider.localScale;
        ScreenEndPoints.x = Screen.width - leftColliderAdjust;
        ScreenEndPoints.y = 0;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(origin), Camera.main.ScreenToWorldPoint(ScreenEndPoints)) * 0.5f;
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.4f), cameraPos.y, zPosition);
        ScreenEndPoints.x = Screen.width ;
        ScreenEndPoints.y = 0;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(origin), Camera.main.ScreenToWorldPoint(ScreenEndPoints)) * 0.5f;
        topCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        topCollider.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topCollider.localScale.y * 0.5f), zPosition);
        bottomCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        bottomCollider.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (bottomCollider.localScale.y * 0.5f), zPosition);
    }
}
