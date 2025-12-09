using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    void Update()
    {
        Vector3 targetPosition = target.position;
        transform.position = targetPosition;
    }
}
