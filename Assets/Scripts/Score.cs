using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreLeft;
    public TMP_Text scoreRight;
    public TMP_Text startGameSign;

    private int counterLeft = 0;
    private int counterRight = 0;

    void Start()
    {
        scoreLeft.text = counterLeft.ToString();
        scoreRight.text = counterRight.ToString();
    }

    void Update()
    {
        
    }

    public void AddScore(string side)
    {
        if (side == "left")
        {
            counterLeft++;
            scoreLeft.text = counterLeft.ToString();
        }
        else
        {
            counterRight++;
            scoreRight.text = counterRight.ToString();
        }
        
    }
    public void SetCounterLeft(int x)
    {
        counterLeft = x;
    }
    public void SetCounterRight(int x)
    {
        counterRight = x;
    }
    public void HideEnterGameSign()
    {
        startGameSign.gameObject.SetActive(false);
    }
        
  
    
        
}
