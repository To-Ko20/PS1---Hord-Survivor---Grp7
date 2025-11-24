using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerManager playerManager;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    public float damageToPlayer;
    [SerializeField] private float enemyHealth;
    
    [SerializeField] private Transform lifeDisplay;

    [SerializeField] private GameObject dataPrefab;

    public bool hasToSlow;
    public bool canMove = true;
    
    private Transform target;
    
    /// Gestion du knockback
    private float knockBackTimer = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        
        canMove = true;
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        
        if (canMove == true)
        {
            if (hasToSlow)
            {
                rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, direction * (speed*PlayerSkillHolderManager.Instance.slowForce), 18f * Time.deltaTime); //déplace l'ennemi vers le joueur
            }
            else
            {
                rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, direction * speed, 18f * Time.deltaTime); //déplace l'ennemi vers le joueur
            }
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
            if (PlayerSkillHolderManager.Instance.rsHasDamages)
            {
                EnemyTakeDamage(PlayerSkillHolderManager.Instance.rsDamages, "rotative shield");
            }
            playerManager.Knockback(PlayerSkillHolderManager.Instance.rsKnockbackForce);
        }
        else if (collision.transform == target.transform)
        {
            playerManager.TakeDamage(damageToPlayer);
        }
    }

    public void EnemyTakeDamage(float amount, string tag)
    {
        enemyHealth -= amount;
        
        if (enemyHealth <= 0)
        {
            EnemyDeath(tag);
        }
    }

    private void EnemyDeath(string tag)
    {
        EnemyManager.Instance.UnregisterEnemy(gameObject);
        if (PlayerSkillHolderManager.Instance.hasMine && tag != "mine")
        {
            if (Random.Range(0f, 1f) <= PlayerSkillHolderManager.Instance.mineRate)
            {
                PlantMine(); 
            }  
        }
        DropData();
        ClickerManager.Instance.DisplayUpdate();
        Destroy(gameObject);
    }

    private void PlantMine()
    {
        GameObject newMine = Instantiate(PlayerSkillHolderManager.Instance.mine, transform.position, Quaternion.identity);
        newMine.transform.SetParent(PlayerSkillHolderManager.Instance.transform);
    }

    private void DropData()
    {
        GameObject dataBubble = Instantiate(dataPrefab, transform.position, Quaternion.identity);
        dataBubble.transform.SetParent(ClickerManager.Instance.transform);
    }
}