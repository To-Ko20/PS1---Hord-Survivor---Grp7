using UnityEngine;

public class MakeAutoclicker : MonoBehaviour
{
    [SerializeField] private GameObject autoClicker;
    
    public void OnUpgradeBought()
    {
        GameObject newAutoClicker = Instantiate(autoClicker);
        ClickerManager.Instance.autoClickers.Add(newAutoClicker);
    }

}
