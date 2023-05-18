using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mobilePhone;

public class Map :MonoBehaviour, Ibutton
{
    [SerializeField] private bool only_ScreenMod;
    

    [SerializeField] private GameObject[] Level_prefabes;
    private GameObject currentLevel;
    private int currentLevel_index;

    private void Start()
    {
       SpwanNewLevel();
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


    public void OnLevelComplete()
    {
        StartCoroutine(levelCompleteRutine());
    }


    private IEnumerator levelCompleteRutine()
    {
        yield return new WaitForSeconds(3);
        SpwanNewLevel();
    }

    [SerializeField] private Phone.keyTypes myKeyType;
        public Phone.keyTypes getMyKeyType()
        {
            return myKeyType;
        }


    private void SpwanNewLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel);
        }

        int index = Random.Range(0,Level_prefabes.Length);

        while(index == currentLevel_index)
        {
            index = Random.Range(0,Level_prefabes.Length);
            
        }
        
        currentLevel = Instantiate(Level_prefabes[index],transform.position,Quaternion.identity);
        currentLevel.transform.parent = transform;

        currentLevel.GetComponent<PeaceGameLevelManger>().Asgin(this);

        currentLevel_index = index;
    }

}
