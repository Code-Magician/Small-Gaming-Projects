using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


[System.Serializable]
public class SavingPaintObjects
{
    public string name;
    public GameObjectData[] savedPaintObjects;


    public SavingPaintObjects(string name, GameObject[] _paintObject)
    {
        this.name = name;

        savedPaintObjects = new GameObjectData[_paintObject.Length];
        for (int i = 0; i < _paintObject.Length; i++)
        {
            GameObject currObj = _paintObject[i];

            savedPaintObjects[i] = new GameObjectData();

            savedPaintObjects[i].name = currObj.name;
            savedPaintObjects[i].position = currObj.transform.position;
            savedPaintObjects[i].rotation = currObj.transform.rotation;


            Mesh currMesh = currObj.GetComponent<MeshFilter>().mesh;

            savedPaintObjects[i].meshName = currMesh.name;
            savedPaintObjects[i].meshVertices = currMesh.vertices;
            savedPaintObjects[i].meshUV = currMesh.uv;
            savedPaintObjects[i].meshTriangles = currMesh.triangles;

            MeshRenderer currMeshRenderer = currObj.GetComponent<MeshRenderer>();
            Color color = currMeshRenderer.material.color;
            savedPaintObjects[i].color = '#' + ColorUtility.ToHtmlStringRGBA(color).Trim();
        }
    }
}


[System.Serializable]
public class GameObjectData
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;

    public string meshName;
    public Vector3[] meshVertices;
    public Vector2[] meshUV;
    public int[] meshTriangles;

    public string color;


    public GameObject GetGameObject()
    {
        GameObject obj = new GameObject(name);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        Mesh mesh = new Mesh();
        mesh.name = meshName;
        mesh.vertices = meshVertices;
        mesh.uv = meshUV;
        mesh.triangles = meshTriangles;

        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        obj.AddComponent<MeshRenderer>();

        return obj;
    }

    public Color GetColor()
    {
        Color savedColor;
        return ColorUtility.TryParseHtmlString(color, out savedColor) ? savedColor : Color.black;
    }
}


public class Painter : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material brushMaterial;
    [Range(0, 1)][SerializeField] float size;

    [SerializeField] GameObject debugObj1;
    [SerializeField] GameObject debugObj2;


    private List<GameObject> paintObjects;
    private Vector3 lastMousePosition;
    private Mesh mesh;
    private float minDist;
    private Color brushColor;


    private void Awake()
    {
        brushMaterial.color = Color.red;
        brushColor = Color.red;

        minDist = size / 2f;
        SavingPaintObjects savedPaintObjects = JsonProvider.Instance.RetrieveData();

        paintObjects = new List<GameObject>();

        if (savedPaintObjects != null)
        {
            foreach (GameObjectData objData in savedPaintObjects.savedPaintObjects)
            {
                GameObject obj = objData.GetGameObject();
                obj.transform.SetParent(this.transform);

                Material newMaterial = new Material(brushMaterial);

                newMaterial.color = objData.GetColor();
                obj.GetComponent<MeshRenderer>().material = newMaterial;

                paintObjects.Add(obj);
            }
        }

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            Vector3 mousePos = GetMousePosition();
            float x = mousePos.x;
            float y = mousePos.y;

            vertices[0] = new Vector3(x - size, y - size);
            vertices[1] = new Vector3(x - size, y + size);
            vertices[2] = new Vector3(x + size, y + size);
            vertices[3] = new Vector3(x + size, y - size);

            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            mesh.name = $"Mesh{paintObjects.Count}";
            mesh.MarkModified();

            GameObject paintObject = new GameObject($"PaintObject{paintObjects.Count}");
            paintObject.transform.SetParent(this.transform);
            paintObjects.Add(paintObject);

            MeshFilter meshFilter = paintObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = paintObject.AddComponent<MeshRenderer>();

            meshFilter.mesh = mesh;
            meshRenderer.material = new Material(brushMaterial);
            meshRenderer.material.color = brushColor;

            lastMousePosition = GetMousePosition();
        }

        if (Input.GetMouseButton(0))
        {
            if (Vector3.Distance(GetMousePosition(), lastMousePosition) > minDist)
            {

                Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
                Vector2[] uv = new Vector2[mesh.uv.Length + 2];
                int[] triangles = new int[mesh.triangles.Length + 6];


                mesh.vertices.CopyTo(vertices, 0);
                mesh.uv.CopyTo(uv, 0);
                mesh.triangles.CopyTo(triangles, 0);


                int vIndex = vertices.Length - 4;
                int vIndex0 = vIndex + 1;
                int vIndex1 = vIndex + 0;
                int vIndex2 = vIndex + 2;
                int vIndex3 = vIndex + 3;

                Vector3 mouseForwardVector = (GetMousePosition() - lastMousePosition).normalized;
                Vector3 normal2D = new Vector3(0, 0, -1f);

                float lineThickness = size;
                Vector3 vectorUp = GetMousePosition() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
                Vector3 vectorDown = GetMousePosition() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;


                debugObj1.transform.position = vectorUp;
                debugObj2.transform.position = vectorDown;

                vertices[vIndex2] = vectorUp;
                vertices[vIndex3] = vectorDown;

                uv[vIndex2] = Vector2.zero;
                uv[vIndex3] = Vector2.zero;


                int tIndex = triangles.Length - 6;
                triangles[tIndex + 0] = vIndex0;
                triangles[tIndex + 1] = vIndex1;
                triangles[tIndex + 2] = vIndex2;

                triangles[tIndex + 3] = vIndex0;
                triangles[tIndex + 4] = vIndex2;
                triangles[tIndex + 5] = vIndex3;


                mesh.vertices = vertices;
                mesh.uv = uv;
                mesh.triangles = triangles;

                lastMousePosition = GetMousePosition();
            }
        }
    }


    public void ChangeCursorColor(Image image)
    {
        brushMaterial.color = image.color;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        return position;
    }


    public void HandleReset()
    {
        foreach (GameObject obj in paintObjects) Destroy(obj);
        paintObjects.Clear();
    }

    public void HandlePaint(Image image)
    {
        brushMaterial.color = image.color;
        brushColor = image.color;
    }

    private void OnApplicationQuit()
    {
        JsonProvider.Instance.SaveData(new SavingPaintObjects(gameObject.name, paintObjects.ToArray()));

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}

