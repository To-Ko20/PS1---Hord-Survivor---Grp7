using UnityEngine;

public class TutorialTriggerZone : MonoBehaviour
{
    [SerializeField] private Collider2D triggerZone;
    [SerializeField] private int        indexToApply;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TutorialManager.Instance.popUpIndex = indexToApply;
            this.gameObject.SetActive(false);
        }
    }
}