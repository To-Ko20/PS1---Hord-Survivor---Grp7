using UnityEngine;

public class IncreaseBulletSize : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        BulletManager.Instance.bulletSize *= 2;
    }
}
