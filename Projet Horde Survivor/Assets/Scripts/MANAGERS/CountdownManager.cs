using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private int remainingMinutes; //in minutes
    [SerializeField] private float remainingSeconds;
    [SerializeField] private int pressureTime; //in minutes
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private QuarantineBehaviors quarantine;
    [SerializeField] private float quarantineTime;
    private float _t;
    public bool isCountdownActive = true;
    
    void FixedUpdate()
    {
        if (isCountdownActive)
        {
            Countdown();
        }
    }

    private void Countdown()
    {
        remainingSeconds -= Time.fixedDeltaTime;
        _t += Time.fixedDeltaTime;


        if (_t >= quarantineTime)
        {
            _t = 0;
            quarantine.Activate();
        }
        
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