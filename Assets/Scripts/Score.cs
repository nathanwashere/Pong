using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TMP_Text scoreLeft;
    public TMP_Text scoreRight;
    public TMP_Text startGameSign;
    public TMP_Text winnerText;
    public GameObject backToMenuButton;


    void Start()
    {
        winnerText.gameObject.SetActive(false);
        backToMenuButton.gameObject.SetActive(false);
        scoreLeft.text = 0.ToString();
        scoreRight.text = 0.ToString();
    }


    public void AddScore(string side)
    {
        if (side == "left")
        {
            GameManager.Instance.CounterLeft++;
            scoreLeft.text = GameManager.Instance.CounterLeft.ToString();
        }
        else
        {
            GameManager.Instance.CounterRight++;
            scoreRight.text = GameManager.Instance.CounterRight.ToString();
        }
        GameManager.Instance.EndGame(winnerText, backToMenuButton);
    }
    public void SetCounterLeft(int x)
    {
        GameManager.Instance.CounterLeft = x;
    }
    public void SetCounterRight(int x)
    {
        GameManager.Instance.CounterRight = x;
    }
    public void HideEnterGameSign()
    {
        startGameSign.gameObject.SetActive(false);
    }   
    public void GoBackToMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame(winnerText, backToMenuButton);
        }

        // Optional: disable ball
        GameObject ball = GameObject.FindWithTag("Ball");
        if (ball != null)
            ball.SetActive(false);

        // Optional: disable paddles
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach (var paddle in paddles)
        {
            paddle.SetActive(false);
        }

        // Optional: disable any other visible stuff (UI, background, etc.)
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas pong");
        if (canvas != null)
            canvas.SetActive(false);

        // Destroy GameManager if you don't need it:
        if (GameManager.Instance != null)
            Destroy(GameManager.Instance.gameObject);


        SceneManager.LoadScene("MainMenu");
    }
}
