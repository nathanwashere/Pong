using UnityEngine;

public class PongSceneManager : MonoBehaviour
{   
    // This class is responsible to start the game when clicking F

    [SerializeField] private Ball ball;

    public bool gameStarted { private set; get; } = false;

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.F)) // --> If game has not started yet and F has been clicked
        {
            gameStarted = true;
            ball.LaunchBallStart();
        }
    }
}
