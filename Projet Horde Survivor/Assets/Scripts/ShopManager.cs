using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject autoClicker;
    [SerializeField] private List<GameObject> autoClickers;
    [SerializeField] private int price;
    [SerializeField] private int autoClickerLevel;
    
    public void Buy(int upgradeID)
    {
        if (ClickerManager.Instance.bits >= price)
        {
            ClickerManager.Instance.bits -= price;
            ClickerManager.Instance.DisplayUpdate();

            switch (upgradeID)
            {
                case 0:
                {
                    GameObject newAutoClicker = Instantiate(autoClicker);
                    autoClickers.Add(newAutoClicker);
                    break;
                }
                case 1:
                {
                    autoClickerLevel++;
                    foreach (GameObject clicker in autoClickers)
                    {
                        clicker.GetComponent<AutoClickerManager>().DecrementSpeed(autoClickerLevel);
                    }

                    break;
                }
                case 2:
                    ClickerManager.Instance.clickPrice *= 2;
                    break;
            }
        }
    }
}
