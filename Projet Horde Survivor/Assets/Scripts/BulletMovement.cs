using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    
    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(0, 1).normalized;
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }
}
