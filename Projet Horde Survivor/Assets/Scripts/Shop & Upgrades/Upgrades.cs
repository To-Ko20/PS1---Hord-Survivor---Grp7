using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrades : MonoBehaviour
{
        public UpgradesSO upgradesSO;
        
        [SerializeField] private TMP_Text titleTxt;
        [SerializeField] private TMP_Text priceTxt;
        [SerializeField] private TMP_Text levelTxt;
        
        [SerializeField] private int level = 0;
        
        [SerializeField] bool isAutoShoot;
        [SerializeField] GameObject[] thingsToActivate;
        [SerializeField] PlayerShoot playerShoot;
        [SerializeField] GameObject musicTrigger;
        
        private readonly List<GameObject> _effectsGo =  new List<GameObject>();
        
        public void OnValidate()
        {
                if (upgradesSO == null)
                        return;
                titleTxt.text = upgradesSO.upgradeName;
                priceTxt.text = upgradesSO.cost[level] + " bits";
                levelTxt.text = "LV " + level;
        }

        public void Start()
        {
                foreach (GameObject effect in upgradesSO.effects)
                {
                        _effectsGo.Add(Instantiate(effect, transform));
                        priceTxt.text = ClickerManager.Instance.ConvertBits(upgradesSO.cost[level]);
                }
        }
        
        public void BuyUpgrade()
        {
                if (level >= upgradesSO.cost.Count) return;
                if (ClickerManager.Instance.bits >= upgradesSO.cost[level])
                {
                        ClickerManager.Instance.bits -= upgradesSO.cost[level];
                        ClickerManager.Instance.DisplayUpdate();
                        level++;
                        
                        if (level < upgradesSO.cost.Count)
                        {
                                priceTxt.text = ClickerManager.Instance.ConvertBits(upgradesSO.cost[level]);
                                levelTxt.text = "LV " + level;
                                
                        }
                        else
                        {
                                priceTxt.text = "SOLD OUT";
                                levelTxt.text = "LV " + "MAX";     
                        }

                        foreach (GameObject effect in _effectsGo)
                        {
                                effect.SendMessage("OnUpgradeBought");
                        }
                        if (isAutoShoot)
                        {
                                foreach (GameObject thing in thingsToActivate)
                                {
                                        thing.SetActive(true);
                                }
                                playerShoot.enabled = true;
                                PlayerSkillHolderManager.Instance.UnlockAutoShoot(musicTrigger);
                                Destroy(gameObject);
                        }
                }       
        }
}
