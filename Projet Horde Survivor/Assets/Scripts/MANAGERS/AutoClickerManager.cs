
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoClickerManager : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private int nbLevel = 51;
    [SerializeField] private Animation anim;
    [SerializeField] private float radius;
    [SerializeField] private int maxNb;
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

    private void Update()
    {
        maxNb = ClickerManager.Instance.clickBTN.transform.childCount;
        SetPosition();
    }

    private void SetPosition()
    {
        int index = transform.GetSiblingIndex();

        if (index < 0 || maxNb <= 0)
            return;

        float angle = index * Mathf.PI * 2f / maxNb;

        Vector3 position = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

        transform.localPosition = position;
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
            anim.Play();
        }
    }

    public void DecrementSpeed(int level)
    {
        speed -= (_baseSpeed-(_baseSpeed/level))/level;
    }
}
