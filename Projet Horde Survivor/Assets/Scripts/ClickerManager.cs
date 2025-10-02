using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    [SerializeField] private int clicks;
    [SerializeField] private long bits;
    [SerializeField] private int clickPrice = 1;
    
    private string[]bitsMetric = new string[6]
    {
        "bits",
        "octet",
        "ko",
        "Mo",
        "Go",
        "To"
    };
    private long[]bitsMetricModulo = new long[6]
    {
        1,
        8,
        1000,
        1000000,
        1000000000,
        1000000000000
    };
    private int bitsMetricIndex = 0;
    
    
    [SerializeField] GameObject autoClicker;
    [SerializeField] List<GameObject> autoClickers;
    
    [SerializeField] private TMP_Text bitsText;
    [SerializeField] private TMP_Text clicksText;

    public static ClickerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
            bits += clickPrice;
            clicks--;
            ConvertBites();
            BitsDisplayUpdate();
            ClicksDisplayUpdate();
        }
    }

    private void ConvertBites()
    {
        if (bits < 8)
        {
            bitsMetricIndex = 0;
        }
        else if (bits < 1000)
        {
            bitsMetricIndex = 1;
        }
        else if (bits < 1000000)
        {
            bitsMetricIndex = 2;
        }
        else if (bits < 1000000000)
        {
            bitsMetricIndex = 3;
        }
        else if (bits < 1000000000000)
        {
            bitsMetricIndex = 4;
        }
        else
        {
            bitsMetricIndex = 5;
        }
    }

    void BitsDisplayUpdate()
    {
        bitsText.text = bits / bitsMetricModulo[bitsMetricIndex] + " " + bitsMetric[bitsMetricIndex];
    }
    
    void ClicksDisplayUpdate()
    {
        clicksText.text = clicks + " clicks";
    }

    public void BuyAutoClicker()
    {
        GameObject newAutoClicker = Instantiate(autoClicker);
        autoClickers.Add(newAutoClicker);
    }

    public void BuyClickerSpeed()
    {
        foreach (GameObject clicker in autoClickers)
        {
            clicker.GetComponent<AutoClickerManager>().DecrementSpeed();
        }
    }

    public void BuyClickerPrice()
    {
        clickPrice *= 2;
    }
}
