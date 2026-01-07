using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Upgrades : MonoBehaviour
{
        public UpgradesSO upgradesSO;
        
        [SerializeField] private TMP_Text titleTxt;
        [SerializeField] private TMP_Text priceTxt;
        [SerializeField] private TMP_Text levelTxt;
        [SerializeField] private Button Btn1;
        [SerializeField] private Button Btn2;
        
        [SerializeField] private int level = 0;
        
        [SerializeField] bool isAutoShoot;
        [SerializeField] GameObject[] thingsToActivate;
        [SerializeField] PlayerShoot playerShoot;

        
        private readonly List<GameObject> _effectsGo =  new List<GameObject>();
        
        public void OnValidate()
        {
                if (upgradesSO == null)
                        return;
                titleTxt.text = upgradesSO.upgradeName;
                priceTxt.text = upgradesSO.cost[level] + " bits";
                levelTxt.text = "LV " + level;
        }

        private void Update()
        {
                if (level >= upgradesSO.cost.Count)
                {
                        Btn1.interactable = false;
                        Btn2.interactable = false;
                        return;
                }
                if (ClickerManager.Instance.bits >= upgradesSO.cost[level])
                {
                        Btn1.interactable = true; 
                        Btn2.interactable = true;
                }
                else
                {
                        Btn1.interactable = false;
                        Btn2.interactable = false;
                }
        }

        public void Start()
        {
                foreach (GameObject effect in upgradesSO.effects)
                {
                        GameObject go = Instantiate(effect, transform);
                        _effectsGo.Add(go);
                        go.transform.SetSiblingIndex(5);
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
                        SoundManager.Instance.PlayBuy();
                        
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
                                UnlockShop();
                        }
                }       
        }

        public void UnlockShop()
        {
                foreach (GameObject thing in thingsToActivate)
                {
                        thing.SetActive(true);
                }
                playerShoot.enabled = true;
                PlayerSkillHolderManager.Instance.UnlockAutoShoot();
                Destroy(gameObject);
        }
}
