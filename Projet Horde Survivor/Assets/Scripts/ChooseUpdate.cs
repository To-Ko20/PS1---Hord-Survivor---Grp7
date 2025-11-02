using TMPro;
using UnityEngine;

public class ChooseUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text chosenUpdateText;
    [SerializeField] GameObject effect;

    public void UpdateSelected()
    {
        DownloadUpdateManager.Instance.SetChosenUpdate(chosenUpdateText, effect);
        UpgradeMenuManager.Instance.HideUpgradeMenu();
    }
}
