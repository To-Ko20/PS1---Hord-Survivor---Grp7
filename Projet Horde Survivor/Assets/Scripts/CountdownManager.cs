using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private int remainingMinutes; //in minutes
    [SerializeField] private float remainingSeconds;
    [SerializeField] private int pressureTime; //in minutes
    [SerializeField] private TMP_Text timerText;
    
    void FixedUpdate()
    {
        Countdown();
    }

    private void Countdown()
    {
        remainingSeconds -= Time.fixedDeltaTime;
        
        if (remainingSeconds <= 0)
        {
            remainingMinutes--;
            remainingSeconds= 59.99f;
        }
        
        if (remainingMinutes < 0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            DisplayCountdownInSeconds();
        }
    }

    private void DisplayCountdownInSeconds()
    {
        timerText.text = remainingMinutes.ToString("00") + ":" + remainingSeconds.ToString("00.00");
    }
}
