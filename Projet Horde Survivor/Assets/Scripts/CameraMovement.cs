using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    public Transform cameraTarget;
    public Vector3 offset = new Vector3(0, 0, 0);
    public float cameraSmoothTime = 0.25f;
    
    private Vector3 _velocity = Vector3.zero;

    public static CameraMovement Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void LateUpdate()
    {
        Vector3 targetPosition = cameraTarget.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, cameraSmoothTime); //déplace la caméra vers le joueur
    }
}