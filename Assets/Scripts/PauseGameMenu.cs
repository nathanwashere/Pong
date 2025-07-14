using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameMenu : MonoBehaviour
{    
    public void GoBackToMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameFromWinnerScene();
        }
        this.enabled = false;

        SceneManager.LoadScene("MainMenu");
    }
}
