using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuSceneCanvas : MonoBehaviour
{
    // This class is responsible for the main menu when you start the game from start

    #region const variables
    private const string MAX_SCORE_FIVE = "5";
    private const string MAX_SCORE_TEN = "10";
    private const string MAX_SCORE_FIFTEEN = "15";
    private const string EASY_DIFFICULTY = "Easy";
    private const string MEDIUM_DIFFICULTY = "Medium";
    private const string HARD_DIFFICULTY = "Hard";
    private const int EASY = 1;
    private const int MEDIUM = 2;
    private const int HARD = 3;
    #endregion

    #region game objects
    [SerializeField] private TMP_Dropdown scoreDropdown;
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject chooseScoreLabelText;
    [SerializeField] private GameObject chooseDifficultyLabelText;
    [SerializeField] private GameObject singlePlayerButton;
    [SerializeField] private GameObject twoPlayerButton;
    #endregion

    // Player choose the options from main menu
    public void PlayGame()
    {
        int indexScore = scoreDropdown.value;
        string selectedOptionScore = scoreDropdown.options[indexScore].text;

        // Choosing max score option
        ChooseMaxScore(selectedOptionScore);

        int indexDifficulty = difficultyDropdown.value;
        string selectedOptionDifficulty = difficultyDropdown.options[indexDifficulty].text;

        // Choosing difficulty
        ChooseDifficulty(selectedOptionDifficulty);

        // Loading the "Pong" scene to play
        SceneManager.LoadScene("Pong");
    }

    // Function that chooses the difficulty
    private static void ChooseDifficulty(string selectedOptionDifficulty)
    {
        if (selectedOptionDifficulty == EASY_DIFFICULTY)
            GameManager.Instance.SetChooseLevel(EASY);
        else if (selectedOptionDifficulty == MEDIUM_DIFFICULTY)
            GameManager.Instance.SetChooseLevel(MEDIUM);
        else if (selectedOptionDifficulty == HARD_DIFFICULTY)
            GameManager.Instance.SetChooseLevel(HARD);
    }

    // Function that chooses the max score in game from dropdown 
    private static void ChooseMaxScore(string selectedOptionScore)
    {
        if (selectedOptionScore == MAX_SCORE_FIVE)
            GameManager.Instance.MaxScore = 5;
        else if (selectedOptionScore == MAX_SCORE_TEN)
            GameManager.Instance.MaxScore = 10;
        else if (selectedOptionScore == MAX_SCORE_FIFTEEN)
            GameManager.Instance.MaxScore = 15;
    }

    // Function that starts the single player game (button)
    public void StartGameSinglePlayer()
    {
        GameManager.Instance.IsSingleplayer = true;
        startGameButton.gameObject.SetActive(true); // --> Showing the start game button
        scoreDropdown.gameObject.SetActive(true);   // --> Showing the score dropdown 
        difficultyDropdown.gameObject.SetActive(true); // --> Showing the difficulty dropdown
        chooseScoreLabelText.gameObject.SetActive(true);    // --> Showing the "choose score" text
        chooseDifficultyLabelText.gameObject.SetActive(true); // --> Showing the "choose difficulty" text
        singlePlayerButton.gameObject.SetActive(false);     // --> Removing the singleplayer button
        twoPlayerButton.gameObject.SetActive(false);        // --> Removing the two player button
    }

    // Function that starts the 2 player game (button)
    public void StartGameTwoPlayer()
    {
        GameManager.Instance.IsSingleplayer = false;
        startGameButton.gameObject.SetActive(true); // --> Showing the start game button
        scoreDropdown.gameObject.SetActive(true);   // --> Showing the score dropdown 
        chooseScoreLabelText.gameObject.SetActive(true);  // --> Showing the "choose score" text
        singlePlayerButton.gameObject.SetActive(false);   // --> Removing the singleplayer button
        twoPlayerButton.gameObject.SetActive(false); // --> Removing the two player button
    }
}
    