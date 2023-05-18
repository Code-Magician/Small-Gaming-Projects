using UnityEngine;





public class Fish : MonoBehaviour
{
    private FishData fishData;
    [SerializeField] FishPart[] fishParts;



    public string GetName
    {
        get
        {
            return gameObject.name;
        }
    }


    public FishData GetFishData
    {
        get
        {
            SaveData();
            return fishData;
        }
    }

    public void SaveData()
    {
        fishData = new FishData();
        fishData.fishPartsData = new FishPartData[fishParts.Length];

        fishData.name = gameObject.name;

        for (int i = 0; i < fishParts.Length; i++)
        {
            fishData.fishPartsData[i] = new FishPartData();
            fishData.fishPartsData[i].name = fishParts[i].name;
            fishData.fishPartsData[i].hexColor = fishParts[i].GetCurrentHexColor();
        }
    }


    public void LoadData(FishData _fishData)
    {
        fishData = _fishData;

        if (fishData != null)
        {
            for (int i = 0; i < fishParts.Length; i++)
            {
                for (int j = 0; j < fishParts.Length; j++)
                {
                    if (fishParts[j].name == fishData.fishPartsData[i].name)
                    {
                        fishParts[j].SetColor(fishData.fishPartsData[i].hexColor);
                    }
                }
            }
        }
        else
        {
            Debug.Log($"{gameObject.name} Null");
            Reset();
        }
    }

    public void Reset()
    {
        foreach (FishPart fp in fishParts) fp.ResetColor();
    }
}
