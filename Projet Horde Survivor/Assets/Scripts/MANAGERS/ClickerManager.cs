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
            bits += clickPrice;
            clicks--;
            DisplayUpdate();
        }
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
}
