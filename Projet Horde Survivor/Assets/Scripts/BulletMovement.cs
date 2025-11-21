using System;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float lifeTime = 3;
    public Vector2 bulletVector;

    public void Start()
    {
        speed = BulletManager.Instance.bulletSpeed;
        transform.localScale = new Vector3(BulletManager.Instance.bulletSize, BulletManager.Instance.bulletSize, 1f);
        bulletDamage = BulletManager.Instance.bulletActualDamage;
        BulletManager.Instance.RecalculateDamage();
        lifeTime = BulletManager.Instance.lifeTime;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + bulletVector * (speed * Time.fixedDeltaTime));
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject enemy in EnemyManager.Instance.activeEnemies)
        {
            if (collision.transform == enemy.transform)
            {
                enemy.GetComponent<EnemyMovement>().EnemyTakeDamage(bulletDamage);
                DestroyBullet();
                return;
            }  
        }
    }

    private void DestroyBullet()
    {
        BulletManager.Instance.bulletList.Remove(gameObject);
        Destroy(gameObject);
    }
}