using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaceGameLevelManger : MonoBehaviour
{
    public List<peace> puzzle_Peaces;
    private Map map;
   
    public static PeaceGameLevelManger instance;
    private void Awake()
    {
        instance = this;
    }


    

    public void checkVectory()
    {
        foreach(peace q in puzzle_Peaces)
        {
            if(!q.mCompleted)
            {
                return;
            }
        }

        // WOn
        print("Won");
        map.OnLevelComplete();
    }

    public void Asgin(Map m)
    {
        map = m;
    }
}
