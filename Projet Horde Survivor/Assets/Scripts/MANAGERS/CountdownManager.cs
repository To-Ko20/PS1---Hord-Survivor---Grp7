using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private int remainingMinutes; //in minutes
    [SerializeField] private float remainingSeconds;
    [SerializeField] private int pressureTime; //in minutes
    [SerializeField] private TMP_Text timerText;
    public bool isCountdownActive = true;
    
    void FixedUpdate()
    {
        if (isCountdownActive == true)
        {
            Countdown();
        }
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
            GameManager.Instance.GameOver();
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