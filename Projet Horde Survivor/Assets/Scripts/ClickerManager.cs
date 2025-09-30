using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    [SerializeField] private float money;
    [SerializeField] private float clickPrice = 1f;
    
    [SerializeField] GameObject autoClicker;
    [SerializeField] List<GameObject> autoClickers;
    
    [SerializeField] private TMP_Text moneyText;

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
        money += clickPrice;
        MoneyDisplayUpdate();
    }

    void MoneyDisplayUpdate()
    {
        moneyText.text = money + " $";
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
        clickPrice += 0.5f;
    }
}
