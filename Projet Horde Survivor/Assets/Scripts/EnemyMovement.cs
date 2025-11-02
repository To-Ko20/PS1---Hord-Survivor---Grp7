using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerManager playerManager;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float damageToPlayer;
    [SerializeField] private float enemyHealth;
    
    [SerializeField] private Transform lifeDisplay;

    [SerializeField] private GameObject dataPrefab; // click prefab
    
    private Transform target;
    
    /// Gestion du knockback
    private bool isKnockedBack = false;
    private float knockBackTimer = 0f;
    [SerializeField] private float knockBackDuration = 1f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }
    
    void Update()
    {
        if (isKnockedBack)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 5f * Time.deltaTime);
            
            knockBackTimer -= Time.deltaTime;
            if (knockBackTimer <= 0f)
            {
                isKnockedBack = false;
            }

            return;
        }
        
        rb.linearVelocity = (target.position - transform.position).normalized * speed; //déplace l'ennemi vers le joueur
    }

    public void ApplyKnockBack(Vector2 direction, float force)
    {
        isKnockedBack = true;
        knockBackTimer = knockBackDuration;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    //Enemy - Player Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == target)
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