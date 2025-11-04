using UnityEngine;

public class IncreaseDlSpeed : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        DownloadUpdateManager.Instance.downloadMaxSpeed *= 2;
    }
}
