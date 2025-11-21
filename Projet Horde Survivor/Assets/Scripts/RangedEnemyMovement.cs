using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedEnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject player;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float       speed;
    [SerializeField] private float       damageToPlayer;
    [SerializeField] private float       enemyHealth;
    [SerializeField] private Transform   firingPoint;
    [SerializeField] private GameObject  bulletPrefab;
    [SerializeField] private float       distanceToShoot;
    [SerializeField] private float       distanceToStop;
    [SerializeField] private float       fireRate;
    private                  float       timeToFire;
    
    [SerializeField] private Transform lifeDisplay;

    [SerializeField] private GameObject dataPrefab;
    
    private Transform target;
    
    private float knockBackTimer = 0f;

    void Start()
    {
        player = PlayerController.Instance.gameObject;
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        timeToFire = fireRate;
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        
        if (Vector2.Distance(transform.position, target.position) > distanceToStop)
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, direction * speed, 18f * Time.deltaTime); //déplace l'ennemi vers le joueur
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (direction != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            var        rotation       = Quaternion.AngleAxis(180, new Vector3(0,0,1)) * targetRotation;
            transform.rotation = rotation;
        }
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= distanceToStop)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            GameObject newBall = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            newBall.GetComponent<EnemyBulletMovement>().player = player;
            timeToFire     = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    public void ApplyKnockBack(Vector2 direction, float force)
    {
        knockBackTimer = playerManager.knockBackDuration;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    //Enemy - Player Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Shield"))
        {
            playerManager.Knockback();
        }
        else if (collision.transform == target.transform)
        {
            playerManager.TakeDamage(damageToPlayer);
        }
    }

    public void EnemyTakeDamage(float ammount)
    {
        enemyHealth -= ammount;

        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        EnemyManager.Instance.UnregisterEnemy(gameObject);
        DropData();
        ClickerManager.Instance.DisplayUpdate();
        Destroy(gameObject);
    }

    private void DropData()
    {
        GameObject dataBubble = Instantiate(dataPrefab, transform.position, Quaternion.identity);
        dataBubble.transform.SetParent(ClickerManager.Instance.transform);
    }
}