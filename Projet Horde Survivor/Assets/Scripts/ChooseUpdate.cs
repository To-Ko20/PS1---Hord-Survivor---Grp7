using TMPro;
using UnityEngine;

public class ChooseUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text chosenUpdateText;
    [SerializeField] GameObject effect;
    public int index;

    public void UpdateSelected()
    {
        DownloadUpdateManager.Instance.SetChosenUpdate(chosenUpdateText);
        UpgradeMenuManager.Instance.StoreSelectedSkill(effect, index, gameObject);
        UpgradeMenuManager.Instance.HideUpgradeMenu();
        
    }
}
