using UnityEngine;

public class DataBubble : MonoBehaviour
{
    public int clicksToGain;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        Debug.Log(other.transform);
        Debug.Log(PlayerController.Instance.transform);
        if (other.transform == PlayerController.Instance.transform)
        {
            ClickerManager.Instance.clicks += clicksToGain;
            Destroy(gameObject);
        }
    }
}
