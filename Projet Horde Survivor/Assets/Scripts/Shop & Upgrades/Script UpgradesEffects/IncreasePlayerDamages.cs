using UnityEngine;

public class IncreasePlayerDamages : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        BulletManager.Instance.bulletDamage *= 5;
    }
}
