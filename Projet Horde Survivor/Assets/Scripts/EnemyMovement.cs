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
    private float _enemyMaxHealth;
    
    [SerializeField] private Transform lifeDisplay;
    private float _lifeDisplayInitSize;

    [SerializeField] private int clicksToGain;
    
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();

        _enemyMaxHealth = enemyHealth;
        _lifeDisplayInitSize =  lifeDisplay.localScale.x;
    }
    
    void Update()
    {
        rb.linearVelocity = (target.position - transform.position).normalized * speed; //déplace l'ennemi vers le joueur
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
        LifeDisplayUpdate();

        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        EnemyManager.Instance.UnregisterEnemy(gameObject);
        ClickerManager.Instance.clicks += clicksToGain;
        ClickerManager.Instance.DisplayUpdate();
        Destroy(gameObject);
    }

    private void LifeDisplayUpdate()
    {
        float lifeDisplayNewSize = (_lifeDisplayInitSize * enemyHealth)/_enemyMaxHealth;
        lifeDisplay.localScale = new Vector3(lifeDisplayNewSize, lifeDisplayNewSize, 1);
    }
}