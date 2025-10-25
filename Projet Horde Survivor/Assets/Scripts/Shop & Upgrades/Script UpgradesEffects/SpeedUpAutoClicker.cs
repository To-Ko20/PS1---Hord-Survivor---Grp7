using UnityEngine;

public class SpeedUpAutoClicker : MonoBehaviour
{
    private int autoClickerLevel = 0;
    
    public void OnUpgradeBought()
    {
        autoClickerLevel++;
        foreach (GameObject autoClicker in ClickerManager.Instance.autoClickers)
        {
            autoClicker.GetComponent<AutoClickerManager>().DecrementSpeed(autoClickerLevel);
        }
    }
}