using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    public long bits;
    public long clickPrice = 1;
    
    [SerializeField] private int clicks;
    [SerializeField] private TMP_Text bitsText;
    [SerializeField] private TMP_Text clicksText;
    
    [SerializeField] private string[]bitsMetric = new string[6]
    {
        "bits",
        "octet",
        "ko",
        "Mo",
        "Go",
        "To"
    };
    [SerializeField] private long[]bitsMetricExponent = new long[6]
    {
        1,
        8,
        1000,
        1_000_000,
        1_000_000_000,
        1_000_000_000_000
    };
    private int _bitsMetricIndex;

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
            bits += clickPrice;
            clicks--;
            DisplayUpdate();
        }
    }

    public void DisplayUpdate()
    {
        ConvertBits();
        BitsDisplayUpdate();
        ClicksDisplayUpdate();
    }

    private void ConvertBits()
    {
        if (bits < 8)
            _bitsMetricIndex = 0;
        else if (bits < 1000)
            _bitsMetricIndex = 1;
        else if (bits < 1_000_000)
            _bitsMetricIndex = 2;
        else if (bits < 1_000_000_000)
            _bitsMetricIndex = 3;
        else if (bits < 1_000_000_000_000)
            _bitsMetricIndex = 4;
        else
            _bitsMetricIndex = 5;
    }

    void BitsDisplayUpdate()
    {
        bitsText.text = bits / bitsMetricExponent[_bitsMetricIndex] + " " + bitsMetric[_bitsMetricIndex];
    }
    
    void ClicksDisplayUpdate()
    {
        clicksText.text = clicks + " clicks";
    }
}
