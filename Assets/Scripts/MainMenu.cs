using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private const string MAX_SCORE_FIVE = "5";
    private const string MAX_SCORE_TEN = "10";
    private const string MAX_SCORE_FIFTEEN = "15";

    public TMP_Dropdown scoreDropdown;
    
    public void PlayGame()
    {
        int index = scoreDropdown.value;
        string selectedOption = scoreDropdown.options[index].text;

        if (selectedOption == MAX_SCORE_FIVE)
            GameManager.Instance.MaxScore = 5;
        else if (selectedOption == MAX_SCORE_TEN)
            GameManager.Instance.MaxScore = 10;
        else if (selectedOption == MAX_SCORE_FIFTEEN)
            GameManager.Instance.MaxScore = 15;

        SceneManager.LoadScene("Pong");
    }
}
