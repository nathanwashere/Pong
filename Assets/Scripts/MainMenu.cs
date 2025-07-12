using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private const string FIVE = "5";
    private const string TEN = "10";
    private const string FIFTEEN = "15";

    public TMP_Dropdown scoreDropdown;
    
    public void PlayGame()
    {
        int index = scoreDropdown.value;
        string selectedOption = scoreDropdown.options[index].text;

        if (selectedOption == FIVE)
            GameManager.Instance.MaxScore = 5;
        else if (selectedOption == TEN)
            GameManager.Instance.MaxScore = 10;
        else if (selectedOption == FIFTEEN)
            GameManager.Instance.MaxScore = 15;

        SceneManager.LoadScene("Pong");
    }
}
