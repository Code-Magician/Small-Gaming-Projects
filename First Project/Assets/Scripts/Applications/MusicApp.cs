using mobilePhone;
using UnityEngine;
using UnityEngine.UI;

public class MusicApp : MonoBehaviour, Ibutton
{
    [SerializeField] private bool onlyScreenMod;
    [SerializeField] private Phone.keyTypes myKeyType;
    [SerializeField] private Sprite[] sprites;


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
        Image image = GetComponent<Image>();
        switch (buttonID)
        {
            case button.buttonIds.one:
                image.sprite = sprites[0];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.two:
                image.sprite = sprites[1];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.three:
                image.sprite = sprites[2];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.four:
                image.sprite = sprites[3];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.five:
                image.sprite = sprites[4];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.six:
                image.sprite = sprites[5];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.seven:
                image.sprite = sprites[6];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.eight:
                image.sprite = sprites[7];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.nine:
                image.sprite = sprites[8];
                Phone.instance.SetScreenMod(true);
                Phone.instance.Spin();
                break;
            case button.buttonIds.red:
                Phone.instance.backToSelection();
                Phone.instance.Spin();
                break;
        }
    }

    public void ChangeInstrument()
    {
        Phone.instance.SetScreenMod(false);
    }
}
