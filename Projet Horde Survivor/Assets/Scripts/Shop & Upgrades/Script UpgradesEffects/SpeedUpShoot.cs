using UnityEngine;

public class SpeedUpShoot : MonoBehaviour
{
    public void OnUpgradeBought()
    {
        PlayerShoot.Instance.countdownToShoot -= 0.0125f;
        // vitesse de tir au niveau 55 est environ 3* supérieure au niveau 1
    }
}