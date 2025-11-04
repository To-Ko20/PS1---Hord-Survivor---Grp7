using DG.Tweening;
using UnityEngine;

public class UiPanelSelector : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private CanvasGroup shopGroup;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private CanvasGroup statsGroup;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private CanvasGroup upgradeGroup;

    public void OpenShopPanel()
    {
        Display(shopGroup, shopPanel, new[] { statsPanel, upgradePanel }, new[] { statsGroup, upgradeGroup });
    }

    public void OpenStatsPanel()
    {
        Display(statsGroup, statsPanel, new[] { shopPanel, upgradePanel }, new[] { shopGroup, upgradeGroup });
    }
    
    public void OpenUpgradePanel()
    {
        Display(upgradeGroup, upgradePanel, new[] { statsPanel, shopPanel }, new[] { statsGroup, shopGroup });
    }

    private void Display(CanvasGroup fadeIn, GameObject panelOn, GameObject[] panelOff, CanvasGroup[] fadeOut)
    {
        foreach (var group in fadeOut)
        {
            if (group.alpha != 0)
            {
                group.DOFade(0, 0.5f).OnComplete(() =>
                {
                    foreach (var panel in panelOff)
                    {
                        panel.SetActive(false);  
                    }
            
                    panelOn.SetActive(true);
                    fadeIn.DOFade(1, 0.5f);
                });
                return;
            }
        }
    }
}
