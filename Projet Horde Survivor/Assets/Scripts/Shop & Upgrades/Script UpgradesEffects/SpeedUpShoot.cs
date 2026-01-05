using UnityEngine;

public class SpeedUpShoot : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        PlayerShoot.Instance.countdownToShoot -= 0.0135f;
        // vitesse de tir au niveau 50 est environ 3* supérieure au niveau 1
    }
}