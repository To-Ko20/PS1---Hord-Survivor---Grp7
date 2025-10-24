using UnityEngine;

public class MakeAutoclicker : MonoBehaviour
{
    [SerializeField] private GameObject autoClicker;
    
    void OnDisable()
    {
        enabled = true;
        GameObject newAutoClicker = Instantiate(autoClicker);
        ClickerManager.Instance.autoClickers.Add(newAutoClicker);
    }

}
