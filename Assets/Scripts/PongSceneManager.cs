using UnityEngine;

public class PongSceneManager : MonoBehaviour
{   
    // This class is responsible to start the game when clicking F

    [SerializeField] private Ball ball;
    [SerializeField] private GameObject paddleRight;

    public bool gameStarted { private set; get; } = false;

    void Start()
    {
      if (GameManager.Instance.IsSingleplayer)
      {
            paddleRight.GetComponent<RightBotPaddle>().enabled = true;
            paddleRight.GetComponent<Paddleright>().enabled = false;
      }
      else
      {
            paddleRight.GetComponent<RightBotPaddle>().enabled = false;
            paddleRight.GetComponent<Paddleright>().enabled = true;
        }
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.F)) // --> If game has not started yet and F has been clicked
        {
            gameStarted = true;
            ball.LaunchBallStart();
        }
    }
}
