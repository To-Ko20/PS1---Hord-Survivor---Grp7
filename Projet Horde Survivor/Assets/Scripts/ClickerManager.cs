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
    
    [SerializeField] private string[]bitsMetric = new string[6]
    {
        "bits",
        "octet",
        "ko",
        "Mo",
        "Go",
        "To"
    };
    [SerializeField] private ulong[]bitsMetricExponent = new ulong[6]
    {
        1,
        8,
        1000,
        1_000_000,
        1_000_000_000,
        1_000_000_000_000
    };
    private int _bitsMetricIndex;
    
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
        switch (bits)
        {
            case < 8:
                _bitsMetricIndex = 0;
                break;
            case < 1000:
                _bitsMetricIndex = 1;
                break;
            case < 1_000_000:
                _bitsMetricIndex = 2;
                break;
            case < 1_000_000_000:
                _bitsMetricIndex = 3;
                break;
            case < 1_000_000_000_000:
                _bitsMetricIndex = 4;
                break;
            default:
                _bitsMetricIndex = 5;
                break;
        }
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
