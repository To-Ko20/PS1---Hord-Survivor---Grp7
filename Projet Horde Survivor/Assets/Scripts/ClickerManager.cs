using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    [SerializeField] private int clicks;
    [SerializeField] private int bits;
    [SerializeField] private int clickPrice = 1;
    
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
            BitsDisplayUpdate();
            ClicksDisplayUpdate();
        }
    }

    void BitsDisplayUpdate()
    {
        bitsText.text = bits + " bits";
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
