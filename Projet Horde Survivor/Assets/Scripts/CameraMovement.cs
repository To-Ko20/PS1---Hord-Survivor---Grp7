using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); //déplace la caméra vers le joueur
    }
}