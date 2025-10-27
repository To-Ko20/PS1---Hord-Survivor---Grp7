using System;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private int bulletDamage;
    public Vector2 bulletVector;

    public void Start()
    {
        speed = BulletManager.Instance.bulletSpeed;
        transform.localScale = new Vector3(BulletManager.Instance.bulletSize, BulletManager.Instance.bulletSize, 1f);
        bulletDamage = BulletManager.Instance.bulletDamage;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + bulletVector * (speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject enemy in EnemyManager.Instance.activeEnemies)
        {
            if (collision.transform == enemy.transform)
            {
                enemy.GetComponent<EnemyMovement>().EnemyTakeDamage(bulletDamage);
                BulletManager.Instance.bulletList.Remove(gameObject);
                Destroy(gameObject);
                return;
            }  
        }
    }
}
