using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private const string LEFT_PLAYER_WINS = "Left Player Wins!";
    private const string RIGHT_PLAYER_WINS = "Right Player Wins!";

    private int maxScore;
    private int leftScore = 0;
    private int rightScore = 0;
    private bool isGameOver = false;

    // In Awake we create the singleton of GameManager (because this object will not be destroyed 
    // when this scene end
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Function that checks if the game has ended
    public void EndGame(TMP_Text winnerText, GameObject backToMenuButton)
    {
        if (!isGameOver && (leftScore == maxScore || rightScore == maxScore))
        {
            isGameOver = true;
            backToMenuButton.gameObject.SetActive(true); // --> Showing the back to menu button
            Time.timeScale = 0f;  // --> Freeze the game
            ShowWinner(winnerText); // --> Shows the winner (text on screen) of the game
        }
    }

    // Function that shows who won as a text on the canvas
    public void ShowWinner(TMP_Text winnerText)
    {
        winnerText.gameObject.SetActive(true);
        if (leftScore > rightScore)
            winnerText.text = LEFT_PLAYER_WINS;
        else
            winnerText.text = RIGHT_PLAYER_WINS;
    }

    // Resetting the game score and data for next game
    public void ResetGameFromWinnerScene()
    {
        leftScore = 0;
        rightScore = 0;
        isGameOver = false;
        Time.timeScale = 1f;
    }

    #region get and set for data
    public int MaxScore
    {
        get => maxScore;
        set => maxScore = value;
    }
    public int CounterLeft
    {
        get => leftScore;
        set => leftScore = value;
    }
    public int CounterRight
    {
        get => rightScore;
        set => rightScore = value;
    }
    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }
    #endregion
}
