using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
   
    private String textPrefix;

    
    private Text textScore;

    private int _score;

   
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            textScore.text = textPrefix + value.ToString();
        }
    }

    
    void Awake()
    {
        textScore = transform.Find("Score").GetComponent<Text>();
        textPrefix = textScore.text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}