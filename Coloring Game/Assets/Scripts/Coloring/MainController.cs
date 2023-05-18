using UnityEngine;
using UnityEngine.UI;

// public enum PARTSNAME
// {
//     Face,
//     Mouth,
//     G1, G2, G3, G4, G5, G6, G7, G8, G9, G10, G11, G12, G13, G14, G15, G16, G17,
//     T1, T2, T3,
//     GUP, GDOWN, GDU
// }


[System.Serializable]
public class AllFishesData
{
    public FishData[] fishesData;
}


[System.Serializable]
public class FishData
{
    public string name;
    public FishPartData[] fishPartsData;
}

[System.Serializable]
public class FishPartData
{
    public string name;
    public string hexColor;
}




public class MainController : MonoBehaviour
{
    [SerializeField] Fish[] allFishes;
    [SerializeField] TMPro.TMP_Text nextButtonText;

    AllFishesData allFishesData;
    Fish activeFish;
    int activeFishIndex = 0, totalFishes;
    Color cursorColor = Color.white;


    private void Start()
    {
        totalFishes = allFishes.Length;

        nextButtonText.text = $"Next ({activeFishIndex + 1}/{totalFishes})";

        if (totalFishes != 0)
            activeFish = allFishes[activeFishIndex];

        SetActiveCurrentFish();
        LoadData();
    }


    public void LoadData()
    {
        allFishesData = JsonProvider.Instance.RetrieveData();

        if (allFishesData != null)
        {
            foreach (FishData fishData in allFishesData.fishesData)
            {
                foreach (Fish fish in allFishes)
                {
                    if (fishData.name == fish.GetName)
                    {
                        fish.LoadData(fishData);
                    }
                }
            }
        }
    }


    public void SaveData()
    {
        allFishesData = new AllFishesData();
        allFishesData.fishesData = new FishData[totalFishes];

        for (int i = 0; i < totalFishes; i++)
        {
            allFishesData.fishesData[i] = allFishes[i].GetFishData;
        }

        JsonProvider.Instance.SaveData(allFishesData);
    }


    public void HandleNextButton()
    {
        activeFishIndex = (activeFishIndex + 1) % totalFishes;
        activeFish = allFishes[activeFishIndex];

        nextButtonText.text = $"Next ({activeFishIndex + 1}/{totalFishes})";

        SetActiveCurrentFish();
    }


    private void SetActiveCurrentFish()
    {
        foreach (Fish x in allFishes)
        {
            x.gameObject.SetActive(false);
        }
        allFishes[activeFishIndex].gameObject.SetActive(true);
    }


    public void ChangeFishPartColor(Image image)
    {
        if (image != null) image.color = cursorColor;
    }


    public void ChangeCursorColor(Image image)
    {
        if (image != null) cursorColor = image.color;
    }


    public void Reset()
    {
        activeFish.Reset();
    }

    private void OnApplicationQuit()
    {
        SaveData();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.quit();
#endif
    }
}
