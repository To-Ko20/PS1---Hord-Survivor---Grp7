using System.Collections.Generic;
using UnityEngine;

public class UpgradesEffects : MonoBehaviour
{
    [SerializeField] private GameObject autoClicker;
    [SerializeField] private List<GameObject> autoClickers;
    
    public void StartEffect(string effect)
    {
        switch (effect)
        {
            case "Auto Clicker":
                GameObject newAutoClicker = Instantiate(autoClicker);
                autoClickers.Add(newAutoClicker);
                break;
        }
    }
}
