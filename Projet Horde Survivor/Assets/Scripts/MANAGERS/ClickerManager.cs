using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    public ulong bits;
    public ulong clickPrice = 1;
    
    public int clicks;
    [SerializeField] private TMP_Text bitsText;
    [SerializeField] private TMP_Text clicksText;
    [SerializeField] private TMP_Text dataRate;
    [SerializeField] private TMP_Text bitsRate;
    
    [SerializeField] private GameObject animUI;
    [SerializeField] private GameObject clickUI;
    [SerializeField] private Animation clickGained;
    [SerializeField] private Animation dataGained;
    
    //private ulong _gainedBits;
    //public int gainedData;
    //private float _elapsed = 0f;
    
    public string[]bitsMetric = new string[6]
    {
        "bits",
        "octet",
        "ko",
        "Mo",
        "Go",
        "To"
    };
    public ulong[]bitsMetricExponent = new ulong[6]
    {
        1,
        8,
        1000,
        1_000_000,
        1_000_000_000,
        1_000_000_000_000
    };
    public int bitsMetricIndex;
    
    public List<GameObject> autoClickers;

    public static ClickerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Click()
    {
        if (clicks > 0)
        {
            clicks--;
            DisplayUpdate();
            GameObject newAnimUI = Instantiate(animUI, transform);
            newAnimUI.transform.SetParent(clickUI.transform);
            Animation anim =  newAnimUI.GetComponent<Animation>();
            anim.Play("ClickAnim");
            StartCoroutine(WaitForAnimation(anim, "ClickAnim", newAnimUI));
            //_gainedBits += clickPrice;
        }
    }
    IEnumerator WaitForAnimation(Animation animation, string animName, GameObject animObj)
    {
        // wait for animation to actually start playing
        yield return null;

        AnimationState state = animation[animName];

        // Wait until the animation is done
        while (animation.IsPlaying(animName))
        {
            yield return null;
        }
        
        bits += clickPrice;
        DisplayUpdate();
        Destroy(animObj);
        clickGained.Play();
    }
    

    public void DisplayUpdate()
    {
        BitsDisplayUpdate();
        ClicksDisplayUpdate();
    }

    public string ConvertBits(ulong bit)
    {
        switch (bit)
        {
            case < 8:
                bitsMetricIndex = 0;
                break;
            case < 1000:
                bitsMetricIndex = 1;
                break;
            case < 1_000_000:
                bitsMetricIndex = 2;
                break;
            case < 1_000_000_000:
                bitsMetricIndex = 3;
                break;
            case < 1_000_000_000_000:
                bitsMetricIndex = 4;
                break;
            default:
                bitsMetricIndex = 5;
                break;
        }
        return bit / bitsMetricExponent[bitsMetricIndex] + " " + bitsMetric[bitsMetricIndex];
    }

    void BitsDisplayUpdate()
    {
        bitsText.text = ConvertBits(bits);
    }
    
    void ClicksDisplayUpdate()
    {
        clicksText.text = clicks + " data";
    }

    /*private void FixedUpdate()
    {
        Countdown();
    }

    private void Countdown()
    {
        _elapsed += Time.fixedDeltaTime;

        if (_elapsed >= 5f)
        {
            _elapsed= 0f;
            _gainedBits = 0;
            gainedData = 0;
        }

        if (Mathf.Floor(_elapsed) % 1f == 0)
        {
            DataRateDisplayUpdate();
            ClickRateDisplayUpdate();
        }
    }

    private void ClickRateDisplayUpdate()
    {
        if (_gainedBits > 0)
        {
            bitsRate.text = (_gainedBits/_elapsed).ToString("F2");
        }
        else
        {
            bitsRate.text = "";
        }
    }

    private void DataRateDisplayUpdate()
    {
        if (gainedData > 0)
        {
            dataRate.text = (gainedData/_elapsed).ToString("F2");
        }
        else
        {
            dataRate.text = "";
        }
    }*/
    public void PlayAnim()
    {
        dataGained.Play();
    }
}
