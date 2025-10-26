using DG.Tweening;
using UnityEngine;

public class UiPanelSelector : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private CanvasGroup shopGroup;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private CanvasGroup statsGroup;

    public void OpenShopPanel()
    {
        
        statsGroup.DOFade(0, 0.5f).OnComplete(() => 
        { 
            statsPanel.SetActive(false);
            shopPanel.SetActive(true);
            shopGroup.DOFade(1, 0.5f);
        });
        
    }

    public void OpenStatsPanel()
    {
        shopGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            shopPanel.SetActive(false); 
            statsPanel.SetActive(true);
            statsGroup.DOFade(1, 0.5f);
        });
        
    }
}
