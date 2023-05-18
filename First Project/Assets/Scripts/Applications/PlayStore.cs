using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mobilePhone;

public class PlayStore : MonoBehaviour, Ibutton
{
    [SerializeField] private bool only_ScreenMod;
        

    public void OpenApp(string link)
    {
        Application.OpenURL(link);
    }

    public void Intereact(button.buttonIds id)
    {
        switch(id)
        {
            case button.buttonIds.red:
    
                Phone.instance.backToSelection();
                break;
                
        }
    }
    
    public bool getScreenMod()
    {
        return only_ScreenMod;
    }

    public void closeApp()
    {
        gameObject.SetActive(false);
    }

    [SerializeField] private Phone.keyTypes myKeyType;
        public Phone.keyTypes getMyKeyType()
        {
            return myKeyType;
        }
  
}
