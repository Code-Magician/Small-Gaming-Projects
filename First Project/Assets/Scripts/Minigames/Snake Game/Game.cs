using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class Game : MonoBehaviour
{
  
    private float time;

    private IEnumerator bonusCoroutine;

  
    private SoundManager soundManager;

    private Snake snake;

    private IntVector2 applePosition;

  
    private IntVector2 bonusPosition;

 
    private bool bonusActive;

 
    private Controller controller;

  
    public MenuPanel Menu;
    public GameObject menu_phone;
   
    public GameOverPanel GameOver;
    public GameObject gameover_phone;
    
    public GamePanel GamePanel;

  
    [Range(0f, 3f)]
    public float GameSpeed;

   
    public Board Board;

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
            GamePanel.Score = value;
            GameOver.Score = value;

            if (value > HighScore)
            {
                HighScore = value;
            }
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
            PlayerPrefs.SetInt("High Score", value);
            GamePanel.HighScore = value;
            Menu.HighScore = value;
        }
    }

    public bool Paused { get; private set; }

    // Use this for initialization
    void Start()
    {
        // Display current high score.
        HighScore = PlayerPrefs.GetInt("High Score", 0);

        // Show main menu
        ShowMenu();

        // Set controller
        controller = GetComponent<Controller>();

        // Creates snake
        snake = new Snake(Board);

        // Pause the game
        Paused = true;

        // Find sound manager
        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        while (time > GameSpeed)
        {
            time -= GameSpeed;
            UpdateGameState();
        }
    }

    private void UpdateGameState()
    {
        if (!Paused && snake != null)
        {
            var dir = controller.NextDirection();

            // New head position
            var head = snake.NextHeadPosition(dir);

            var x = head.x;
            var y = head.y;

            if (snake.WithoutTail.Contains(head))
            {
                // Snake has bitten its tail - game over
                StartCoroutine(GameOverCoroutine());
                return;
            }

            if (x >= 0 && x < Board.Columns && y >= 0 && y < Board.Rows)
            {
                if (head == applePosition)
                {
                    soundManager.PlayAppleSoundEffect();
                    snake.Move(dir, true);
                    Score += 1;
                    PlantAnApple();
                }
                else if (head == bonusPosition && bonusActive)
                {
                    soundManager.PlayBonusSoundEffect();
                    snake.Move(dir, true);
                    Score += 10;
                    StopCoroutine(bonusCoroutine);
                    PlantABonus();
                }
                else
                {
                    snake.Move(dir, false);
                }
            }
            else
            {
                // Head is outside board's bounds - game over.
                StartCoroutine(GameOverCoroutine());
            }
        }
    }

    /// <summary>
    /// Shows main menu.
    /// </summary>
    public void ShowMenu()
    {
        HideAllPanels();
        Menu.gameObject.SetActive(true);
        menu_phone.SetActive(true);
    }

    /// <summary>
    /// Shows game over panel.
    /// </summary>
    public void ShowGameOver()
    {
        HideAllPanels();
        GameOver.gameObject.SetActive(true);
        gameover_phone.SetActive(true);
    }

    /// <summary>
    /// Shows the board and starts the game.
    /// </summary>
    public void StartGame()
    {
        HideAllPanels();
        Restart();
        GamePanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides all panels.
    /// </summary>
    private void HideAllPanels()
    {
        Menu.gameObject.SetActive(false);
        menu_phone.SetActive(false);
        GamePanel.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);
        gameover_phone.SetActive(false);
    }

    /// <summary>
    /// Returns game to initial conditions.
    /// </summary>
    private void Restart()
    {
        // Resets the controller.
        controller.Reset();

        // Set score
        Score = 0;

        // Disable bonus
        bonusActive = false;

        // Clear board
        Board.Reset();

        // Resets snake
        snake.Reset();

        // Plant an apple
        PlantAnApple();

        // Start bonus coroutine
        PlantABonus();

        // Start the game
        Paused = false;
        time = 0;
    }

 
    private void PlantABonus()
    {
        bonusActive = false;
        bonusCoroutine = BonusCoroutine();
        StartCoroutine(bonusCoroutine);
    }


    private void PlantAnApple()
    {
        if (Board[applePosition].Content == TileContent.Apple)
        {
            Board[applePosition].Content = TileContent.Empty;
        }

        var emptyPositions = Board.EmptyPositions.ToList();
        if (emptyPositions.Count == 0)
        {
            return;
        }
        applePosition = emptyPositions.RandomElement();
        Board[applePosition].Content = TileContent.Apple;
    }

  
    private IEnumerator BonusCoroutine()
    {
        // Wait for a random period of time
        yield return new WaitForSeconds(Random.Range(GameSpeed * 20, GameSpeed * 40));

        // Put a bonus on a board at a random place
        var emptyPositions = Board.EmptyPositions.ToList();
        if (emptyPositions.Count == 0)
        {
            yield break;
        }
        bonusPosition = emptyPositions.RandomElement();
        Board[bonusPosition].Content = TileContent.Bonus;
        bonusActive = true;

        // Wait
        yield return new WaitForSeconds(GameSpeed * 16);

        // Start bonus to blink
        for (int i = 0; i < 5; i++)
        {
            Board[bonusPosition].ContentHidden = true;
            yield return new WaitForSeconds(GameSpeed * 1.5f);
            Board[bonusPosition].ContentHidden = false;
            yield return new WaitForSeconds(GameSpeed * 1.5f);
        }

        // Remove a bonus and restart the coroutine
        bonusActive = false;
        Board[bonusPosition].Content = TileContent.Empty;

        bonusCoroutine = BonusCoroutine();
        yield return StartCoroutine(bonusCoroutine);
    }

    
    private IEnumerator GameOverCoroutine()
    {
      
        soundManager.PlayGameOverSoundEffect();

     
        StopCoroutine(bonusCoroutine);

      
        Paused = true;

       
        for (int i = 0; i < 3; i++)
        {
            snake.Hide();
            yield return new WaitForSeconds(GameSpeed * 1.5f);
            snake.Show();
            yield return new WaitForSeconds(GameSpeed * 1.5f);
        }

       
        ShowGameOver();
    }
}
