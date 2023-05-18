using UnityEngine;
using System.IO;

public class JsonProvider
{
    private static JsonProvider instance = null;

    private JsonProvider() { }

    public static JsonProvider Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JsonProvider();
            }
            return instance;
        }
    }

    string path = Application.dataPath + "/StaticJsons/output.json";

    public void SaveData(AllFishesData data)
    {
        string jsonString = JsonUtility.ToJson(data);

        Debug.Log(path);
        Debug.Log(jsonString);

        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(jsonString);
        writer.Close();
    }

    public AllFishesData RetrieveData()
    {
        if (!File.Exists(path)) return null;

        StreamReader reader = new StreamReader(path);
        string jsonString = reader.ReadToEnd();
        reader.Close();

        AllFishesData data = JsonUtility.FromJson<AllFishesData>(jsonString);

        return data;
    }
}
