using TMPro;
using UnityEngine;

public class StatisticsDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text autoclickerNb;
    [SerializeField] private TMP_Text autoclickerSpeed;
    [SerializeField] private TMP_Text clickValue;
    [SerializeField] private TMP_Text shootSpeed;
    [SerializeField] private TMP_Text maxDlSpeed;

    // Update is called once per frame
    void Update()
    {
       autoclickerNb.text = ClickerManager.Instance.autoClickers.Count.ToString();
       if (ClickerManager.Instance.autoClickers.Count != 0)
       {
           autoclickerSpeed.text = ClickerManager.Instance.autoClickers[0].GetComponent<AutoClickerManager>().speed + "s";
       }
       else
       {
           autoclickerSpeed.text = "No auto clicker";
       }
       clickValue.text = ClickerManager.Instance.ConvertBits((ulong) ClickerManager.Instance.clickPrice);
       shootSpeed.text = PlayerShoot.Instance.countdownToShoot + "s";
       maxDlSpeed.text = ClickerManager.Instance.ConvertBits((ulong) DownloadUpdateManager.Instance.downloadMaxSpeed);
    }
}