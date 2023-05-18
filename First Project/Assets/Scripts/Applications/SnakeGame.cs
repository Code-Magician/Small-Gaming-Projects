using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mobilePhone;

public class SnakeGame : MonoBehaviour, Ibutton
{
    [SerializeField] private bool only_ScreenMod;
    [SerializeField] private GameObject game;

    private Controller snakeGameControles;

  
    void Start()
    {
        game.SetActive(true);
        snakeGameControles = game.GetComponent<Controller>();
    }


    void Update()
    {
        
    }

    public void Intereact(button.buttonIds tag)
    {
        switch(tag)
            {
                case button.buttonIds.red:
    
                    Phone.instance.backToSelection();
                    break;

                case button.buttonIds.green:
                   

                    break;
                
                case button.buttonIds.center:

                    break;
                
                case button.buttonIds.one:
                   snakeGameControles.Move(Vector2.left);
                    break;
                
                case button.buttonIds.two:
                    snakeGameControles.Move(Vector2.up);
                    break;

                case button.buttonIds.three:
                    snakeGameControles.Move(Vector2.right);
                    break;

                case button.buttonIds.four:
                    snakeGameControles.Move(Vector2.left);
                    break;

            
                
                case button.buttonIds.six:
                    snakeGameControles.Move(Vector2.right);
                    break;
                
                case button.buttonIds.seven:
                    snakeGameControles.Move(Vector2.left);
                    break;

                case button.buttonIds.eight:
                    snakeGameControles.Move(Vector2.down);
                    break;
                
                case button.buttonIds.nine:
                    snakeGameControles.Move(Vector2.right);
                    break;
            }
    }

    [SerializeField] private Phone.keyTypes myKeyType;
        public Phone.keyTypes getMyKeyType()
        {
            return myKeyType;
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
