using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mobilePhone;

public class Chrome : MonoBehaviour, Ibutton
{
    [SerializeField] private bool only_ScreenMod;
    [SerializeField] private Phone.keyTypes myKeyType;
    [SerializeField] private GameObject jurasicRunPrefabe;
    private GameObject currentGame;
    private Level LevelManger;
    
    void Start()
    {
        newGame();
    }

    void Update()
    {
        
    }


    private void newGame()
    {
        Time.timeScale = 1;
        if(currentGame != null)
        {
            Destroy(currentGame);
        }

        currentGame = Instantiate(jurasicRunPrefabe);
        LevelManger = currentGame.GetComponent<Level>();
        
    }

    public void Intereact(button.buttonIds tag)
    {
        switch(tag)
            {
                case button.buttonIds.red:
                    endGame();
                    Phone.instance.backToSelection();
                    break;

                case button.buttonIds.green:
                   

                    break;
                
                case button.buttonIds.center:
                    if(LevelManger.isOut())
                    {
                        newGame();
                    }
                    else
                    {
                        LevelManger.onKeyPressed();
                    }
                    
                    break;
               
            }
    }


        public Phone.keyTypes getMyKeyType()
        {
            return myKeyType;
        }
    public void endGame()
    {
        Time.timeScale = 1;
        if(currentGame != null)
        {
            Destroy(currentGame);
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

}
