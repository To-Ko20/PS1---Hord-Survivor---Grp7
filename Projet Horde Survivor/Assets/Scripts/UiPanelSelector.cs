using DG.Tweening;
using UnityEngine;

public class UiPanelSelector : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private CanvasGroup shopGroup;
    
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private CanvasGroup clickGroup;
    
    [SerializeField] private bool isShopOpen = false;
    
    [SerializeField] private KeyCode menuKey = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            SwapPanel();
        }
    }

    public void SwapPanel()
    {
        if (isShopOpen)
        {
            shopGroup.DOFade(0, 0.5f).OnComplete(() =>
            {
                shopPanel.SetActive(false); 
                clickPanel.SetActive(true);
                clickGroup.DOFade(1, 0.5f);
            });
            isShopOpen = false;
        }
        else
        {
            clickGroup.DOFade(0, 0.5f).OnComplete(() =>
            {
                clickPanel.SetActive(false); 
                shopPanel.SetActive(true);
                shopGroup.DOFade(1, 0.5f);
            });
            isShopOpen = true;
        }
    }
}
