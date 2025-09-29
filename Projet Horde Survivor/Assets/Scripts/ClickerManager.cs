using System.Collections.Generic;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    [SerializeField] private float money = 0f;
    [SerializeField] private float clickPrice = 1f;
    
    [SerializeField] GameObject autoClicker;
    [SerializeField] List<GameObject> autoClickers;

    public void Click()
    {
        money += clickPrice;
    }

    public void BuyAutoClicker()
    {
        GameObject newAutoClicker = Instantiate(autoClicker);
        autoClickers.Add(newAutoClicker);
    }

    public void BuyClickerSpeed()
    {
        foreach (GameObject autoClicker in autoClickers)
        {
            autoClicker.GetComponent<AutoClickerManager>().DecrementSpeed();
        }
    }

    public void BuyClickerPrice()
    {
        clickPrice += 0.5f;
    }
}
