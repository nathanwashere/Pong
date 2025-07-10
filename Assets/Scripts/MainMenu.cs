using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public TMP_Dropdown scoreDropdown;
    
    private int maxScore;

    void Start()
    {
     
    }


    void Update()
    {
        
    }

    public void PlayGame()
    {
        int index = scoreDropdown.value;
        string selectedOption = scoreDropdown.options[index].text;

        if (selectedOption == "5")
            GameManager.Instance.MaxScore = 5;
        else if (selectedOption == "10")
            GameManager.Instance.MaxScore = 10;
        else if (selectedOption == "15")
            GameManager.Instance.MaxScore = 15;

        SceneManager.LoadScene("Pong");
    }

}
