using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseGameCanvas;
    [SerializeField] private GameObject gameplayObjects;
    [SerializeField] private Ball ball;

    private bool isPaused = false;

    void Start()
    {
        pauseGameCanvas.SetActive(false);   // Hide pause menu at start
        gameplayObjects.SetActive(true);    // Show gameplay UI and objects at start    
        //  Time.timeScale = 1f;  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    } 


    private void TogglePause()
    {
        if (!isPaused)
        {
            // **1) snapshot BEFORE disabling the root**
            ball.StoreVelocity();

            pauseGameCanvas.SetActive(true);
            gameplayObjects.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            pauseGameCanvas.SetActive(false);
            gameplayObjects.SetActive(true);
            Time.timeScale = 1f;

            // **2) restore AFTER re‑enabling the root**
            ball.RestoreVelocity();
        }
        isPaused = !isPaused;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // change to your menu scene name
    }
}
