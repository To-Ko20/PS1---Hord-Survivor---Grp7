using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerManager playerManager;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float damageToPlayer;
    [SerializeField] private float currentHealth;
    [SerializeField] private int maxHealth;
    
    private Transform target;

    void Start()
    {
        currentHealth = maxHealth;
        
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }
    
    void Update()
    {
        rb.linearVelocity = (target.position - transform.position).normalized * speed; //déplace l'ennemi vers le joueur
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == target)
        {
            playerManager.TakeDamage(damageToPlayer);

            TakeDamage(playerManager.damage);
            
            enemySpawner.activeEnemies.Remove(gameObject);
            //Debug.Log("Active enemies : " + enemySpawner.activeEnemies.Count);
        }
    }

    private void TakeDamage(float ammount)
    {
        currentHealth -= ammount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}