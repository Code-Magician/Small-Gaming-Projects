using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
  
    private String textPrefix;

   
    private Text textHighScore;

    private int _highScore;

  
    public int HighScore
    {
        get
        {
            return _highScore;
        }
        set
        {
            _highScore = value;
            textHighScore.text = textPrefix + value.ToString();
        }
    }

    // Use this for initialization
    void Awake()
    {
        textHighScore = transform.Find("High Score").GetComponent<Text>();
        textPrefix = textHighScore.text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
