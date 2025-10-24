using UnityEngine;

public class SpeedUpAutoClicker : MonoBehaviour
{
    private int autoClickerLevel = 1;
    void OnDisable()
    {
        enabled = true;
        foreach (GameObject autoClicker in ClickerManager.Instance.autoClickers)
        {
            autoClicker.GetComponent<AutoClickerManager>().DecrementSpeed(autoClickerLevel);
        }
    }
}
