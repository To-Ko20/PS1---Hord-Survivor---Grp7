using System;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float   speed;
    [SerializeField] private float   bulletDamage;
    private float timeToDespawn = 10f;
    private Vector2 target;
    public GameObject player;

    void Start()
    {
        target = (player.transform.position -  transform.position).normalized;
        Debug.DrawRay(rb.position, target, Color.red);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + target * (speed * Time.fixedDeltaTime));
        
        timeToDespawn -= Time.fixedDeltaTime;
        if (timeToDespawn <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 otherPos =  other.transform.position;
        if (otherPos == target)
        {
            PlayerManager.Instance.TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}