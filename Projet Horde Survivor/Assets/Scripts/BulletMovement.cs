using System;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float lifeTime = 3;
    public Vector2 bulletVector;
    [SerializeField] private bool isShard = false;
    [SerializeField] private GameObject parentEnemy;
    [SerializeField] private GameObject explodeShard;
    public new CircleCollider2D collider2D;

    public void Start()
    {
        speed = BulletManager.Instance.bulletSpeed;
        transform.localScale = new Vector3(BulletManager.Instance.bulletSize, BulletManager.Instance.bulletSize, 1f);
        bulletDamage = BulletManager.Instance.bulletActualDamage;
        BulletManager.Instance.RecalculateDamage(1);
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
                if (parentEnemy != null)
                {
                    if (collision.gameObject != parentEnemy)
                    {
                        if (PlayerSkillHolderManager.Instance.hasExplosiveShoot && !isShard)
                        {
                            Explode(collision.transform.position, enemy);
                        }

                        DealDamages(enemy);
                        DestroyBullet();
                    }
                }
                else 
                {
                    if (PlayerSkillHolderManager.Instance.hasExplosiveShoot && !isShard)
                    {
                        Explode(collision.transform.position, enemy);
                    }
                    DealDamages(enemy);
                    DestroyBullet();
                }
            }
        }
    }

    private void DealDamages(GameObject target)
    {
        if (PlayerSkillHolderManager.Instance.hasCursedShot)
        {
            if (PlayerManager.Instance.currentHealth > (PlayerManager.Instance.maxHealth * 0.15))
            {
                target.GetComponent<EnemyMovement>().EnemyTakeDamage(bulletDamage*2, "bullet");
                PlayerManager.Instance.TakeDamage(2);
            }
            else
            {
                target.GetComponent<EnemyMovement>().EnemyTakeDamage(bulletDamage, "bullet");
            }
        }
        else
        {
            target.GetComponent<EnemyMovement>().EnemyTakeDamage(bulletDamage, "bullet");
        }
    }

    private void Explode(Vector3 spawn, GameObject Enemy)
    {
        int explodeNb = PlayerSkillHolderManager.Instance.explodeNb;
        float rotation = 360f / explodeNb;
        for (int i = 0; i < explodeNb; i++)
        {
            GameObject newBullet = Instantiate(explodeShard, spawn, Quaternion.identity);
            BulletMovement newBulletMovement = newBullet.GetComponent<BulletMovement>();
            newBulletMovement.parentEnemy = Enemy;
            Vector3 dir = Vector3.up.normalized;
            dir = Quaternion.Euler(0, 0, rotation*i) * dir;
            
            newBulletMovement.bulletVector = dir;
            BulletManager.Instance.bulletList.Add(newBullet);
            newBulletMovement.collider2D.enabled = true;
        }
    }
    private void DestroyBullet()
    {
        BulletManager.Instance.bulletList.Remove(gameObject);
        Destroy(gameObject);
    }
}