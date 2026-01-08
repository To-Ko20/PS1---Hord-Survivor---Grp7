
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AutoClickerManager : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private int nbLevel = 51;
    [SerializeField] private Animation anim;
    [SerializeField] private float radius;
    [SerializeField] private int maxNb;
    [SerializeField] private Vector2 anchor;
    private float _baseSpeed;
    private float _timer;
    private GameObject _parent;
    private Image _image;
    [SerializeField]private Sprite btn;
    [SerializeField]private Sprite pressedBtn;

    private void Start()
    {
        _parent = ClickerManager.Instance.clickBTN.transform.gameObject;
        _baseSpeed = speed;
        _image = _parent.GetComponent<Image>();
        if (ClickerManager.Instance.autoClickers.Count > 1)
        {
            speed = ClickerManager.Instance.autoClickers[0].GetComponent<AutoClickerManager>().speed;
        }
    }

    private void Update()
    {
        maxNb = _parent.transform.childCount;
        SetPosition();
    }

    private void SetPosition()
    {
        int index = transform.GetSiblingIndex();

        if (index < 0 || maxNb <= 0)
            return;

        float angle = index * Mathf.PI * 2f / maxNb;

        Vector3 position = new Vector3(Mathf.Cos(angle) * radius + anchor.x, Mathf.Sin(angle) * radius + anchor.y, 0f);

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
            _image.sprite = pressedBtn;
            StartCoroutine(WaitForSeconds(0.1f));
            _timer = 0f;
            ClickerManager.Instance.Click();
            anim.Play();
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _image.sprite = btn;
    }

    public void DecrementSpeed(int level)
    {
        speed -= (_baseSpeed-(_baseSpeed/nbLevel))/nbLevel;
    }
}
