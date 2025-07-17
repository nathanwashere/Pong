using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PongMainCanvas : MonoBehaviour
{
    #region game objects
    [SerializeField] private TMP_Text scoreLeft;
    [SerializeField] private TMP_Text scoreRight;
    [SerializeField] private TMP_Text startGameSign;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private GameObject backToMenuButton;
    #endregion

    void Start()
    {
        winnerText.gameObject.SetActive(false); // --> Disables the "Winner!" text
        backToMenuButton.gameObject.SetActive(false); // --> Disables the "Back to main menu" button

        // Setting the text of score to be 0 at the start of the game
        scoreLeft.text = 0.ToString();  
        scoreRight.text = 0.ToString();
    }

    // Adding score via string (left or right) through the GameMangaer singleton
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

    #region get and set for counter
    public void SetCounterLeft(int x)
    {
        GameManager.Instance.CounterLeft = x;
    }
    public void SetCounterRight(int x)
    {
        GameManager.Instance.CounterRight = x;
    }
    #endregion

    // Hide the "Press F to start game" text
    public void HideEnterGameSign()
    {
        startGameSign.gameObject.SetActive(false);
    }   

    // Go back to menu when you are in winner state (someone won and the game ended)
    public void GoBackToMenu()
    {
        // Here we are "hiding" all the game objects from pong scene in order to make
        // smooth transition between scenes

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameFromWinnerScene(); // Resetting all the score and etc
        }

        //GameObject ball = GameObject.FindWithTag("Ball");
        //if (ball != null)
        //    ball.SetActive(false);

        //GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        //foreach (var paddle in paddles)
        //{
        //    paddle.SetActive(false);
        //}

        //GameObject canvas = GameObject.FindGameObjectWithTag("Gameplay");
        //if (canvas != null)
        //    canvas.SetActive(false);

        SceneManager.LoadScene("MainMenu"); // Going back to main menu scene
    }
}
