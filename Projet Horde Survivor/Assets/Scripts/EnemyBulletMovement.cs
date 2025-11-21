using System;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float   speed;
    [SerializeField] private float   bulletDamage;
    public Vector2 bulletVector;
    private float timeToDespawn = 10f;
    private Transform target;

    void Start()
    {
        target       = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        bulletVector = new Vector2(target.position.x, target.position.y);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + bulletVector * (speed * Time.fixedDeltaTime));
    }

    void Update()
    {
        timeToDespawn -= Time.deltaTime;
        if (timeToDespawn <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.Instance.TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}