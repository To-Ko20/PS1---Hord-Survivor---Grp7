using UnityEngine;

public class IncreaseClickPrice : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        ClickerManager.Instance.clickPrice *= 3;
    }
}