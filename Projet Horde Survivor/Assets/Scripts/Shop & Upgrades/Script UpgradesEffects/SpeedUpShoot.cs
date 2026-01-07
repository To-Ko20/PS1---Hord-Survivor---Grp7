using UnityEngine;

public class SpeedUpShoot : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        PlayerShoot.Instance.countdownToShoot -= 0.0135f;
    }
}