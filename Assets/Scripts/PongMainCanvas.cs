using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PongMainCanvas : MonoBehaviour
{
    // This class is responsible for the pong canvas when playing pong (pong scene)

    #region game objects
    [SerializeField] private TMP_Text scoreLeft;
    [SerializeField] private TMP_Text scoreRight;
    [SerializeField] private TMP_Text startGameSign;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private GameObject backToMenuButton;
    [SerializeField] private GameObject startAgainGame;
    [SerializeField] private PongSceneManager pongSceneManager;
    #endregion

    void Start()
    {
        winnerText.gameObject.SetActive(false); // --> Disables the "Winner!" text
        backToMenuButton.gameObject.SetActive(false); // --> Disables the "Back to main menu" button
        startAgainGame.gameObject.SetActive(false); // --> Disables the "Start again" button

        // Setting the text of score to be 0 at the start of the game
        scoreLeft.text = 0.ToString();  
        scoreRight.text = 0.ToString();
    }

    void Update()
    {
        if (pongSceneManager.gameStarted) // --> Hide the "Press F to start game" text
        {
            startGameSign.gameObject.SetActive(false);
        }
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
        GameManager.Instance.EndGame(winnerText, backToMenuButton, startAgainGame);
    }

    // Go back to menu when you are in winner state (someone won and the game ended)
    public void GoBackToMenu()
    {
        // Here we are "hiding" all the game objects from pong scene in order to make
        // smooth transition between scenes

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameSettingFromWinnerScene(); // Resetting all the score and etc
        }
        
        // Loading scene "MainMenu" to start the whole game again
        SceneManager.LoadScene("MainMenu"); // Going back to main menu scene
    }

    // Function that starts the  game again from the winner menu, with all the same settings
    public void StartGameAgainFromWinnerMenu()
    {
        GameManager.Instance.ResetGameSettingFromWinnerScene(); // --> Resetting data

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // --> Resetting the actual game
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
}
