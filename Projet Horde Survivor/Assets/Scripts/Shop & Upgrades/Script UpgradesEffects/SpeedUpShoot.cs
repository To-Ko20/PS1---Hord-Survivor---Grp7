using UnityEngine;

public class SpeedUpShoot : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        PlayerShoot.Instance.countdownToShoot /= 2;
    }
}
