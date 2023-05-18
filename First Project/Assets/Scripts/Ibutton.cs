using mobilePhone;

public interface Ibutton
{
    void Intereact(button.buttonIds buttonID);

    bool getScreenMod();

    void closeApp();

    Phone.keyTypes getMyKeyType();
}
