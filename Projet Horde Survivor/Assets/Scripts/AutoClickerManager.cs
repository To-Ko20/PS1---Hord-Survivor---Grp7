using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoClickerManager : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private float _timer;

    void FixedUpdate()
    {
        Timer();
    }

    private void Timer()
    {
        if (_timer <= speed)
        {
            _timer += Time.fixedDeltaTime;
        }
        else
        {
            _timer = 0f;
            ClickerManager.instance.Click();
        }
    }

    public void DecrementSpeed()
    {
        speed /= 2;
    }
}
