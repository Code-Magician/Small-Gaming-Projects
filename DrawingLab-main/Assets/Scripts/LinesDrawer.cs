using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TypeOfPen
{
    Create,
    Magnet,
    Box,
    Food,
    Monster,
    Water,
    Rope,
    Destroy,
    SBCircle,
    SBSquare,
    SBRect,
    SBTri,
    SBPent,
    SBPlus,
    SBCar,
    SBWater
}
public class LinesDrawer : MonoBehaviour
{
    public GraphicRaycaster graphicraycaster;
    public SaveGameObject savegameobject;
    public TypeOfPen penType;
    public GameObject linePrefab, MagnetPrefab, boxprefab, foodprefab, monsterprefab, waterprefab, Ropeprefab, SBCirlceprefab, SBSquareprefab, SBRectprefab, SBTriprefab, Eraserprefab, SBPentprefab, SBPlusprefab, SBCarprefab, ropePreviewprefab, SBWaterprefab;
    public LayerMask cantDrawOverLayer, DestroyLayer;
    int cantDrawOverLayerIndex;

    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance, BoxandFoodspawn;
    public float lineWidth;
    public Line currentLine;
    public bool chosingColor, iscreating;
    GameObject currentMagnet, currentEraser;
    Vector2 updatemousepos, updatetouchpos;
    Camera cam;
    float boxfoodspawntime, magnetSize, boxSize, foodSize, monsterSize, waterSize, SBCSize, SBSSize, SBRSize, SBTSize, SBPenSize, SBPlusSize, SBCarSize, SBWaterSize;


    List<RaycastResult> results;
    PointerEventData ped;
    private List<GameObject> AllObjects = new List<GameObject>();
    void Start()
    {
        cam = Camera.main;
        savegameobject = GetComponent<SaveGameObject>();
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        boxfoodspawntime = BoxandFoodspawn;

        magnetSize = MagnetPrefab.transform.localScale.x;
        boxSize = boxprefab.transform.localScale.x * 2;
        foodSize = foodprefab.transform.localScale.x * 2;
        monsterSize = monsterprefab.transform.localScale.x;
        waterSize = waterprefab.transform.localScale.x / 2;
        SBCSize = SBCirlceprefab.transform.localScale.x;
        SBSSize = SBSquareprefab.transform.localScale.x;
        SBRSize = SBRectprefab.transform.localScale.x;
        SBTSize = SBTriprefab.transform.localScale.x;
        SBPenSize = SBPentprefab.transform.localScale.x;
        SBPlusSize = SBPlusprefab.transform.localScale.x;
        SBCarSize = SBCarprefab.transform.localScale.x;
        SBWaterSize = 1;
        //eraserSize = Eraserprefab.transform.localScale.x;

        currentMagnet = null;
        currentEraser = null;

        results = new List<RaycastResult>();
        ped = new PointerEventData(null);

        SaveGameObject svGmobj = GetComponent<SaveGameObject>();
        if (svGmobj == null)
            return;
        svGmobj.LevelNumber = DataManager.instance.LeveltoLoad;
        svGmobj.LoadChildren();
    }

    void Update()
    {
        boxfoodspawntime -= Time.deltaTime;
        if (chosingColor)
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            updatetouchpos = cam.ScreenToWorldPoint(touch.position);

            results.Clear();
            //Set required parameters, in this case, mouse position
            ped.position = touch.position;
            //Raycast ui items
            graphicraycaster.Raycast(ped, results);

            if (results.Count > 0)
            {
                EndDraw(updatetouchpos, true);
                return;
            }

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    BeginDraw(updatetouchpos);
                    break;

                case TouchPhase.Stationary:
                    BeginDraw(updatetouchpos, true);
                    break;
                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    Draw(updatetouchpos);
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    EndDraw(updatetouchpos);
                    break;
            }
        }

        /*updatemousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
            BeginDraw(updatemousepos);

        if (Input.GetMouseButton(0))
            Draw(updatemousepos);

        if (Input.GetMouseButtonUp(0))
            EndDraw(updatemousepos);*/
    }

    // Begin Draw ----------------------------------------------
    void BeginDraw(Vector2 position, bool stationary = false)
    {
        if (penType == TypeOfPen.Destroy)
            raycastDestroy(position);
        else if (penType == TypeOfPen.Magnet && currentMagnet == null)
            raycastMagnet(MagnetPrefab, magnetSize, position);
        else if (penType == TypeOfPen.Box)
            raycastMagnet(boxprefab, boxSize, position, stationary);
        else if (penType == TypeOfPen.Food)
            raycastMagnet(foodprefab, foodSize, position, stationary);
        else if (penType == TypeOfPen.Monster)
            raycastMagnet(monsterprefab, monsterSize, position);
        else if (penType == TypeOfPen.Water)
            raycastMagnet(waterprefab, waterSize, position, stationary);
        else if (penType == TypeOfPen.Rope && stationary == false)
            createrope(position);
        else if (penType == TypeOfPen.SBCircle)
            raycastMagnet(SBCirlceprefab, SBCSize, position, stationary, true);
        else if (penType == TypeOfPen.SBSquare)
            raycastMagnet(SBSquareprefab, SBSSize, position, stationary, true);
        else if (penType == TypeOfPen.SBRect)
            raycastMagnet(SBRectprefab, SBRSize, position, stationary, true);
        else if (penType == TypeOfPen.SBTri)
            raycastMagnet(SBTriprefab, SBTSize, position, stationary, true);
        else if (penType == TypeOfPen.SBPent)
            raycastMagnet(SBPentprefab, SBPenSize, position, stationary, true);
        else if (penType == TypeOfPen.SBPlus)
            raycastMagnet(SBPlusprefab, SBPlusSize, position, stationary, true);
        else if (penType == TypeOfPen.SBCar)
            raycastMagnet(SBCarprefab, SBCarSize, position, false, true);
        else if (penType == TypeOfPen.SBWater)
            raycastMagnet(SBWaterprefab, SBWaterSize, position, stationary);
        if (linePrefab == null || ismouseOverui.ismouseover || iscreating)
            return;
        iscreating = true;
        currentLine = Instantiate(linePrefab, transform).GetComponent<Line>();
        AllObjects.Add(currentLine.gameObject);
        //Set line properties
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);

    }

    Vector2 ropestart, ropeend;
    public int rope_n = 0;
    GameObject ropePreview;

    void createrope(Vector2 position)
    {
        if (ismouseOverui.ismouseover)
        {
            rope_n = 0;
            Destroy(ropePreview);
            return;
        }
        if (rope_n == 0)
        {
            ropestart = position;
            rope_n = 1;
            if (!ropePreview)
            {
                ropePreview = Instantiate(ropePreviewprefab, Vector3.zero, Quaternion.identity, transform);
                LineRenderer lr = ropePreview.GetComponent<LineRenderer>();
                lr.positionCount = 1;
                lr.SetPosition(0, ropestart);
                lr.colorGradient = lineColor;
            }
        }
        else if (rope_n == 1)
        {
            ropeend = position;
            rope_n = 0;
            if (Vector2.Distance(ropestart, ropeend) < Mathf.Epsilon)
                return;
            rope_n = 0;
            GameObject rope_ins = Instantiate(Ropeprefab, ropestart, Quaternion.identity, transform);
            AllObjects.Add(rope_ins);
            rope_ins.GetComponent<Rope>().generateRope(ropestart, ropeend, lineColor.colorKeys[0].color);
            Destroy(ropePreview);
        }
    }

    private void raycastMagnet(GameObject prefab, float prefabSize, Vector2 position, bool stationary = false, bool cancolorchange = false)
    {
        if (stationary)
        {
            if (boxfoodspawntime > 0)
                return;
            else
                boxfoodspawntime = BoxandFoodspawn;
        }
        Vector2 mousePosition = position; //cam.ScreenToWorldPoint(Input.mousePosition)

        //Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, prefabSize, Vector2.zero, 1f, DestroyLayer);

        if (!hit && !ismouseOverui.ismouseover)
        {
            GameObject temp = Instantiate(prefab, mousePosition, Quaternion.identity, transform);
            AllObjects.Add(temp);
            if (cancolorchange)
                temp.GetComponent<SpriteRenderer>().color = lineColor.colorKeys[0].color;
            if (penType == TypeOfPen.Magnet)
                currentMagnet = temp;
        }
    }

    private void raycastDestroy(Vector2 position)
    {
        Vector2 mousePosition = position; //cam.ScreenToWorldPoint(Input.mousePosition)

        if (!currentEraser)
            currentEraser = Instantiate(Eraserprefab, mousePosition, Quaternion.identity, transform);

        if (!ismouseOverui.ismouseover)
        {
            currentEraser.SetActive(true);
            currentEraser.transform.position = mousePosition;
        }
        else
            currentEraser.SetActive(false);

        mousePosition = currentEraser.transform.Find("Raycast_origin").position;
        //Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, DestroyLayer);

        if (hit)
        {
            currentEraser.SetActive(true);
            if (hit.collider.gameObject.CompareTag("SoftBody"))
            {
                Destroy(hit.transform.parent.gameObject);
                return;
            }
            Destroy(hit.collider.gameObject);
        }
    }

    // Draw ----------------------------------------------------
    void Draw(Vector2 position)
    {
        Vector2 mousePosition = position; //cam.ScreenToWorldPoint(Input.mousePosition)
        if (penType == TypeOfPen.Destroy)
        {
            raycastDestroy(position);
            return;
        }
        else if (penType == TypeOfPen.Water)
            raycastMagnet(waterprefab, waterSize, position, true);
        else if (penType == TypeOfPen.SBWater)
            raycastMagnet(SBWaterprefab, SBWaterSize, position, true);
        else if (penType == TypeOfPen.Magnet && currentMagnet != null)
        {
            currentMagnet.transform.position = mousePosition;
        }
        else if (penType == TypeOfPen.Box)
            raycastMagnet(boxprefab, boxSize, position, true);
        else if (penType == TypeOfPen.Food)
            raycastMagnet(foodprefab, foodSize, position, true);
        else if (penType == TypeOfPen.Rope)
            if (ropePreview)
            {
                LineRenderer lr = ropePreview.GetComponent<LineRenderer>();
                if (lr.positionCount < 2)
                    lr.positionCount = 2;
                lr.SetPosition(1, position);
            }
        if (currentLine == null)
            return;

        //Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        // if (hit || ismouseOverui.ismouseover)
        //  EndDraw(position);
        //else
        currentLine.AddPoint(mousePosition);
    }
    // End Draw ------------------------------------------------
    void EndDraw(Vector2 position, bool calledthroughraycast = false)
    {
        if (penType == TypeOfPen.Destroy)
        {
            raycastDestroy(position);
            Destroy(currentEraser);
            return;
        }
        else if (penType == TypeOfPen.Magnet && currentMagnet != null)
        {
            Destroy(currentMagnet);
            return;
        }
        else if (calledthroughraycast == false && penType == TypeOfPen.Rope && !ismouseOverui.ismouseover)
            createrope(position);
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                //If line has one point
                Destroy(currentLine.gameObject);
                currentLine = null;
            }
            else
            {
                //Add the line to "CantDrawOver" layer
                currentLine.gameObject.layer = cantDrawOverLayerIndex;

                //Activate Physics on the line
                currentLine.UsePhysics(true);

                currentLine = null;
            }
            iscreating = false;
        }
    }

}