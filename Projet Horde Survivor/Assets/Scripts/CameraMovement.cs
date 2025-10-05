using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}