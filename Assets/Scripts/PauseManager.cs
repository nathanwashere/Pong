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
        Time.timeScale = 1f;  // --> when you press from pause menu to restart the game
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // --> Show pause menu when clicking Escape
            TogglePause();
    } 

    // Function to show the pause menu or unpause when clicked on escape
    private void TogglePause()
    {
        if (!isPaused)
        {
            // Before freezing and chaning canvas, we save the velocity of the ball in order 
            // to keep the velocity the same after player clicks again escape or 
            // clicks on resume
            ball.StoreVelocity();

            // Changing canvases
            pauseGameCanvas.SetActive(true); 
            gameplayObjects.SetActive(false);
            
            // Freezing the game
            Time.timeScale = 0f;
        }
        else
        {
            // Chaning canvases
            pauseGameCanvas.SetActive(false);
            gameplayObjects.SetActive(true);

            // Unfreezing the game
            Time.timeScale = 1f;

            // Restoring the balls velocity like it was before
            ball.RestoreVelocity();
        }
        isPaused = !isPaused;
    }

    // Restarting game when in pause game
    public void RestartGame()
    {
        // Unfreezing the game
        Time.timeScale = 1f; 

        // Loading again the game from beginning
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Continuing the game if click on resume in pause menu
    public void ContinueGame()
    {
        isPaused = true;

        // TogglePause because we need to unfreeze the game and change canvases
        TogglePause();
    }

    // Load the main menu when clicked in pause menu
    public void GoBackToMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameFromWinnerScene(); // --> Resetting score and more
        }

        SceneManager.LoadScene("MainMenu"); // --> Loading the main menu scene
    }

}
