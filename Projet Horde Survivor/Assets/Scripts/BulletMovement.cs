using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    public Vector2 bulletVector;
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + bulletVector * (speed * Time.fixedDeltaTime));
    }
}
