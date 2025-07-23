using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    // This class is responsible for the main menu when you start the game from start

    #region const variables
    private const string MAX_SCORE_FIVE = "5";
    private const string MAX_SCORE_TEN = "10";
    private const string MAX_SCORE_FIFTEEN = "15";
    #endregion

    public TMP_Dropdown scoreDropdown;
    
    // Player choose the options from main menu
    public void PlayGame()
    {
        int index = scoreDropdown.value;
        string selectedOption = scoreDropdown.options[index].text;
        
        // Choosing max score option
        if (selectedOption == MAX_SCORE_FIVE)
            GameManager.Instance.MaxScore = 5;
        else if (selectedOption == MAX_SCORE_TEN)
            GameManager.Instance.MaxScore = 10;
        else if (selectedOption == MAX_SCORE_FIFTEEN)
            GameManager.Instance.MaxScore = 15;

        // Loading the "Pong" scene to play
        SceneManager.LoadScene("Pong");
    }
}
    