using System;
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
    private float knockBackTimer = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, direction * speed, 18f * Time.deltaTime); //déplace l'ennemi vers le joueur

        if (direction != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            var        rotation       = Quaternion.AngleAxis(180, new Vector3(0,0,1)) * targetRotation;
            transform.rotation = rotation;
        }
    }
    
    /// Ralentissement du temps on hit ///
    
    // Coroutine lerpingTimeCoroutine = null;
        
    /*IEnumerator LerpingTime(float durationFirst,float secondDuration, float targetTimeFirst, float targetTimeSecond)
    {
        float time = 0;
        while (time < durationFirst)
        {
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
            float newTimeScale = Mathf.Lerp(Time.timeScale, targetTimeFirst, time);
            Time.timeScale = newTimeScale;
        }
        Time.timeScale = targetTimeFirst;
            
        time = 0;
            
        while (time < secondDuration)
        {
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
            float newTimeScale = Mathf.Lerp(Time.timeScale, targetTimeSecond, time);
            Time.timeScale = newTimeScale;
        }
        Time.timeScale = targetTimeSecond;
    }*/

    public void ApplyKnockBack(Vector2 direction, float force)
    {
        knockBackTimer = playerManager.knockBackDuration;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse); 
        //lerpingTimeCoroutine = StartCoroutine(LerpingTime(0.1f, 0.4f, playerManager.knockBackSlowTime, 1f));
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