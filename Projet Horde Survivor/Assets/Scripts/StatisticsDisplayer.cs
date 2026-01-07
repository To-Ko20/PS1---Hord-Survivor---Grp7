using TMPro;
using UnityEngine;

public class StatisticsDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text autoclickerNb;
    [SerializeField] private TMP_Text autoclickerSpeed;
    [SerializeField] private TMP_Text clickValue;
    [SerializeField] private TMP_Text shootSpeed;
    [SerializeField] private TMP_Text maxDlSpeed;
    [SerializeField] private TMP_Text damages;
    
    void Update()
    {
       autoclickerNb.text = "Amount : " + ClickerManager.Instance.autoClickers.Count;
       if (ClickerManager.Instance.autoClickers.Count != 0)
       {
           autoclickerSpeed.text = ClickerManager.Instance.autoClickers[0].GetComponent<AutoClickerManager>().speed + "s";
       }
       else
       {
           autoclickerSpeed.text = "No autoclicker installed";
       }
       clickValue.text = "1 data = " +ClickerManager.Instance.ConvertBits((ulong) ClickerManager.Instance.clickPrice);
       shootSpeed.text = PlayerShoot.Instance.countdownToShoot + "s";
       maxDlSpeed.text = ClickerManager.Instance.ConvertBits((ulong) DownloadUpdateManager.Instance.downloadMaxSpeed) + "/s";
       damages.text = BulletManager.Instance.bulletDamage + " damages";
    }
}