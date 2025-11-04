using UnityEngine;

public class DataBubble : MonoBehaviour
{
    [SerializeField] private int clicksToGain;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform == PlayerController.Instance.transform)
        {
            ClickerManager.Instance.clicks += clicksToGain;
            ClickerManager.Instance.DisplayUpdate();
            Destroy(gameObject);
        }
    }
}
