using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClickerManager : MonoBehaviour
{
    public float speed = 2f;
    private float timer = 0f;
    
    [SerializeField] ClickerManager clickerManager;

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (timer <= speed)
        {
            timer += Time.fixedDeltaTime; 
        }
        else
        {
            timer = 0f;
            clickerManager.Click();
        }
    }

    public void DecrementSpeed()
    {
        speed -= 0.5f;
    }
}
