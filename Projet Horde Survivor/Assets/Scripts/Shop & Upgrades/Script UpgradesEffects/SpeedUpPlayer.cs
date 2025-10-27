using UnityEngine;

public class SpeedUpPlayer : MonoBehaviour
{
    [SerializeField] private float speedUpPercentValue = 0.1f;
    
    public void OnUpgradeBought()
        {
            PlayerController.Instance.playerSpeed += PlayerController.Instance.playerSpeed * speedUpPercentValue;
            CameraMovement.Instance.cameraSmoothTime -= CameraMovement.Instance.cameraSmoothTime *  (speedUpPercentValue/2);
        }
}
