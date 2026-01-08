using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    private GameObject musicTrigger;
    
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerManager playerManager;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    public float damageToPlayer;
    public float enemyHealth;
    
    [SerializeField] private Transform lifeDisplay;

    [SerializeField] private GameObject dataPrefab;
    
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material glitchMaterial;

    public bool hasToSlow;
    public bool canMove = true;

    private bool canTakeDamages = true;
    
    private Transform target;
    
    /// Gestion du knockback
    private float knockBackTimer = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        musicTrigger = SoundManager.Instance.musicTrigger;

        normalMaterial = EnemyManager.Instance.normalMaterial;
        glitchMaterial = EnemyManager.Instance.glitchMaterial;
        
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Axe"))
        {
            EnemyTakeDamage(BulletManager.Instance.bulletActualDamage/2, "axe");

        }
    }

    public void EnemyTakeDamage(float amount, string tag)
    {
        if (canTakeDamages)
        {
            canTakeDamages = false;
            if (musicTrigger.activeSelf == false)
            {
                if (!TutorialManager.Instance.isTutorialOn)
                {
                    musicTrigger.SetActive(true); 
                }
            }
            StartCoroutine(DmgAnimation());
            enemyHealth -= amount;
        
            if (enemyHealth <= 0)
            {
                EnemyDeath(tag);
            } 
        }
    }

    IEnumerator DmgAnimation()
    {
        var renderer = GetComponent<Renderer>();
        yield return new WaitForSeconds(0.0625f);
        renderer.material = glitchMaterial;
        yield return new WaitForSeconds(0.0625f);
        renderer.material = normalMaterial;
        yield return new WaitForSeconds(0.0625f);
        renderer.material = glitchMaterial;
        yield return new WaitForSeconds(0.0625f);
        renderer.material = normalMaterial;
        canTakeDamages = true;

    }
    
    IEnumerator DeathAnimation()
    {
        var renderer = GetComponent<Renderer>();
        yield return new WaitForSeconds(0.0625f);
        renderer.material = glitchMaterial;
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        yield return new WaitForSeconds(0.03125f);
        renderer.material = normalMaterial;
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        yield return new WaitForSeconds(0.0625f);
        renderer.material = glitchMaterial;
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        Destroy(gameObject);
    }

    // ReSharper disable Unity.PerformanceAnalysis
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

        if (!TutorialManager.Instance.isTutorialOn)
        {
            DropData();
        }
        
        if (PlayerSkillHolderManager.Instance.hasVampire && tag != "bullet")
        {
            if (Random.Range(0,1) <= PlayerSkillHolderManager.Instance.vampireRate)
            PlayerManager.Instance.GainLife(10);
        }
        
        StartCoroutine(DeathAnimation());
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