using UnityEngine;

public class IncreaseClickPrice : MonoBehaviour
{
    void OnDisable()
    {
        enabled = true;
        ClickerManager.Instance.clickPrice *= 2;
        Destroy(gameObject);
    }

}
