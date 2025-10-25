using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrades : MonoBehaviour
{
        [SerializeField] private UpgradesSO upgradesSO;
        
        [SerializeField] private TMP_Text titleTxt;
        [SerializeField] private TMP_Text priceTxt;
        [SerializeField] private TMP_Text levelTxt;
        
        [SerializeField] private int level = 0;
        
        private readonly List<GameObject> _effectsGo =  new List<GameObject>();
        
        private void OnValidate()
        {
                if (upgradesSO == null)
                        return;
                titleTxt.text = upgradesSO.upgradeName;
                priceTxt.text = upgradesSO.cost[level] + " bits";
                levelTxt.text = "LV " + level;
        }

        private void Start()
        {
                foreach (GameObject effect in upgradesSO.effects)
                {
                        _effectsGo.Add(Instantiate(effect, transform));
                } 
        }
        
        public void BuyUpgrade()
        {
                if (ClickerManager.Instance.bits >= upgradesSO.cost[level])
                {
                        ClickerManager.Instance.bits -= upgradesSO.cost[level];
                        ClickerManager.Instance.DisplayUpdate();
                        level++;
                        
                        if (level <= upgradesSO.cost.Count - 1)
                        {
                                priceTxt.text = upgradesSO.cost[level] + " bits";
                                levelTxt.text = "LV " + level;
                                foreach (GameObject effect in _effectsGo)
                                {
                                        effect.SendMessage("OnUpgradeBought");
                                } 
                        }
                        else
                        {
                                priceTxt.text = "SOLD OUT";
                                levelTxt.text = "LV " + "MAX";     
                        }
                }       
        }
}
