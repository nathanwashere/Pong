using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PongMainCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLeft;
    [SerializeField] private TMP_Text scoreRight;
    [SerializeField] private TMP_Text startGameSign;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private GameObject backToMenuButton;




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
        if (winnerText != null)
            winnerText.gameObject.SetActive(false);

        if (backToMenuButton != null)
            backToMenuButton.SetActive(false);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameFromWinnerScene();
        }

        GameObject ball = GameObject.FindWithTag("Ball");
        if (ball != null)
            ball.SetActive(false);

        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach (var paddle in paddles)
        {
            paddle.SetActive(false);
        }

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas pong");
        if (canvas != null)
            canvas.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}
