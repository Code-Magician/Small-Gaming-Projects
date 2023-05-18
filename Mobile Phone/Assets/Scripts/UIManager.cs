using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text numpadText;


    public void HandleButtons(string value)
    {
        numpadText.text = numpadText.text + value;
    }

    public void HandleBackSpace()
    {
        if (numpadText.text.Length < 1) return;

        numpadText.text = (numpadText.text).Substring(0, numpadText.text.Length - 1);
    }

    public void HandleBackButton()
    {
        numpadText.text = "";
    }
}
