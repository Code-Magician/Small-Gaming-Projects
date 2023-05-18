using UnityEngine;
using mobilePhone;
using UnityEngine.UI;

public class Torch : MonoBehaviour, Ibutton
{
    [SerializeField] private bool onlyScreenMod;
    [SerializeField] private Phone.keyTypes myKeyType;
    [SerializeField] private bool isOn = false;

    [SerializeField] private Sprite flashOnSprite, flashOffSprite;

    private void Start()
    {
        Phone.instance.OpenApplication(this.gameObject);
    }

    public void closeApp()
    {
        gameObject.SetActive(false);
    }

    public Phone.keyTypes getMyKeyType()
    {
        return myKeyType;
    }

    public bool getScreenMod()
    {
        return onlyScreenMod;
    }

    public void Intereact(button.buttonIds buttonID)
    {
        throw new System.NotImplementedException();
    }

    public void ToggleFlash()
    {
        isOn = !isOn;

        if (isOn) GetComponent<Image>().sprite = flashOnSprite;
        else GetComponent<Image>().sprite = flashOffSprite;
    }

    public void BackToSelection()
    {
        Phone.instance.backToSelection();
    }
}
