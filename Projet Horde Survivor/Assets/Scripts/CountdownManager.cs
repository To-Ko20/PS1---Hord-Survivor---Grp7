using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private int remainingMinutes; //in minutes
    [SerializeField] private float remainingSeconds;
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
            remainingSeconds= 60;
        }
        
        if (remainingMinutes == 0)
        {
            DisplayCountdownInMilliseconds();
        }
        else if (remainingMinutes <= 0)
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
        timerText.text = remainingMinutes.ToString("00") + ":" + remainingSeconds.ToString("00");
    }
    
    private void DisplayCountdownInMilliseconds()
    {
        timerText.text = remainingSeconds.ToString("00") + ":" + remainingSeconds.ToString("#.00");
    }
}
