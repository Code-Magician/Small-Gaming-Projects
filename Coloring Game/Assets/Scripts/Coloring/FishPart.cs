using UnityEngine;
using UnityEngine.UI;

public class FishPart : MonoBehaviour
{
    [SerializeField] private string partName;
    [SerializeField] private Image image;


    private void Awake()
    {
        image = GetComponent<Image>();

        // Color currColor;
        // string currColorHexCode = PlayerPrefs.GetString(partName.ToString().Trim()).Trim();
        // if (ColorUtility.TryParseHtmlString(currColorHexCode, out currColor))
        //     image.color = currColor;
    }

    public string GetCurrentHexColor()
    {
        Debug.Log((image == null) ? "Null" : "Not");
        Color savingColor = image.color;
        return ("#" + ColorUtility.ToHtmlStringRGBA(savingColor).Trim());
    }

    public void SetColor(string hexColor)
    {
        Color currColor;
        if (ColorUtility.TryParseHtmlString(hexColor, out currColor))
            image.color = currColor;
    }

    // public void SaveColor()
    // {
    //     Color savingColor = image.color;
    //     string savingColorHexCode = "#" + ColorUtility.ToHtmlStringRGBA(savingColor).Trim();

    //     PlayerPrefs.SetString(partName.ToString().Trim(), savingColorHexCode);
    // }



    public void ResetColor()
    {
        image.color = Color.white;
    }
}
