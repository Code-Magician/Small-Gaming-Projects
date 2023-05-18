using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
  
    private Text textScore;
  
    private Text textHighScore;

    private int _score;
    private int _highScore;

  
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            textScore.text = value.ToString();
        }
    }

    public int HighScore
    {
        get
        {
            return _highScore;
        }
        set
        {
            _highScore = value;
            textHighScore.text = value.ToString();
        }
    }


    void Awake () {
        textScore = transform.Find("Score").GetComponent<Text>();
        textHighScore = transform.Find("High Score").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
