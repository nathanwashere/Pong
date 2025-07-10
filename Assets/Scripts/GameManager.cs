using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int maxScore;
    private int leftScore = 0;
    private int rightScore = 0;
    private bool isGameOver = false;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EndGame(TMP_Text winnerText, GameObject backToMenuButton)
    {
        if (!isGameOver && (leftScore == maxScore || rightScore == maxScore))
        {
            isGameOver = true;
            backToMenuButton.gameObject.SetActive(true);
            Time.timeScale = 0f;
            ShowWinner(winnerText);
        }
    }
    public void ShowWinner(TMP_Text winnerText)
    {
        winnerText.gameObject.SetActive(true);
        if (leftScore > rightScore)
            winnerText.text = "Left Player Wins!";
        else
            winnerText.text = "Right Player Wins!";
    }
    public void ResetGame(TMP_Text winnerText, GameObject backToMenuButton)
    {
        leftScore = 0;
        rightScore = 0;
        isGameOver = false;
        Time.timeScale = 1f;

        if (winnerText != null)
            winnerText.gameObject.SetActive(false);

        if (backToMenuButton != null)
            backToMenuButton.SetActive(false);
    }



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
}
