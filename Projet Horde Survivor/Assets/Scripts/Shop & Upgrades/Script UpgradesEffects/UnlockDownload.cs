using UnityEngine;

public class UnlockDownload : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        DownloadUpdateManager.Instance.UnlockDownload();
    }
}
