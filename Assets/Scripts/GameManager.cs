using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This class represent one game "manager" - singleton - that is not being destroyed when switching scenes
    // so that we can keep data throughout all the scenes (like score)

    public static GameManager Instance { get; private set; }

    #region const
    private const string LEFT_PLAYER_WINS = "Left Player Wins!";
    private const string RIGHT_PLAYER_WINS = "Right Player Wins!";
    private const float paddleSpeedEasy = 5f;
    private const float paddleSpeedMedium = 5f;
    private const float paddleSpeedHard = 12f;
    private const float reactionTimeEasy = 0.3f;
    private const float reactionTimeMedium = 0.2f;
    private const float reactionTimeHard = 0.05f;
    private const float errorMarginEasy = 1.0f;
    private const float errorMarginMedium = 0.5f;
    private const float errorMarginHard = 0.1f;
    private const float deadZoneEasy = 0.5f;
    private const float deadZoneMedium = 0.2f;
    private const float deadZoneHard = 0.1f;
    #endregion

    private int maxScore;
    private int leftScore = 0;
    private int rightScore = 0;
    private bool isGameOver = false;
    private bool isSingleplayer;  // Maybe dont need it
    private float[] easyLevel = { paddleSpeedEasy, reactionTimeEasy, errorMarginEasy, deadZoneEasy };
    private float[] mediumLevel = { paddleSpeedMedium, reactionTimeMedium, errorMarginMedium, deadZoneMedium };
    private float[] hardLevel = { paddleSpeedHard, reactionTimeHard, errorMarginHard, deadZoneHard };
    private int chooseLevel;

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
    public void EndGame(TMP_Text winnerText, GameObject backToMenuButton, GameObject startAgainGame)
    {
        if (!isGameOver && (leftScore == maxScore || rightScore == maxScore))
        {
            isGameOver = true;
            backToMenuButton.gameObject.SetActive(true); // --> Showing the "Back to menu" button
            startAgainGame.gameObject.SetActive(true); // --> Showing the "Start again" button
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

    // Function that sets choose level
    public void SetChooseLevel(int chooseLevel)
    {
        this.chooseLevel = chooseLevel;
    }

    // Function that returns the array of difficulty
    public float[] GetLevel()
    {
        if (chooseLevel == 1)
        {
            return easyLevel;
        }
        else if (chooseLevel == 2)
        {
            return mediumLevel;
        }
        else if (chooseLevel == 3)
        {
            return hardLevel;
        }
        Debug.Log("LEVEL IS NULL! INSIDE GET LEVEL IN GAME MANAGER SCRIPT");
        return null;
    }
    // Resetting the game score and data for next game
    public void ResetGameSettingFromWinnerScene()
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
    public bool IsSingleplayer
    {
        get => isSingleplayer;
        set => isSingleplayer = value;
    }
    #endregion
}
