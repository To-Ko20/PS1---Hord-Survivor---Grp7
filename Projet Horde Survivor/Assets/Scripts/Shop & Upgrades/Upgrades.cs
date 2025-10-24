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
                        Instantiate(effect, transform);
                } 
        }
        
        public void BuyUpgrade()
        {
                if (ClickerManager.Instance.bits >= upgradesSO.cost[level] && level <= upgradesSO.cost.Count-1)
                {
                        
                        ClickerManager.Instance.bits -= upgradesSO.cost[level];
                        ClickerManager.Instance.DisplayUpdate();
                        
                        level++;
                        priceTxt.text = upgradesSO.cost[level] + " bits";
                        levelTxt.text = "LV " + level;
                        
                        foreach (GameObject effect in upgradesSO.effects)
                        {
                                effect.GetComponent<MonoBehaviour>().enabled = false;
                        } 
                }       
        }
}
