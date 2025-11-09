using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoClickerManager : MonoBehaviour
{
    public float speed = 2f;
    private float _baseSpeed;
    private float _timer;

    private void Start()
    {
        _baseSpeed = speed;
        if (ClickerManager.Instance.autoClickers.Count > 1)
        {
            speed = ClickerManager.Instance.autoClickers[0].GetComponent<AutoClickerManager>().speed;
        }
    }

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
            ClickerManager.Instance.Click();
        }
    }

    public void DecrementSpeed(int level)
    {
        speed = _baseSpeed/(Mathf.Pow(2, level));
    }
}
