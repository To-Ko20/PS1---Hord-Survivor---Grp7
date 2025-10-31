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

    [SerializeField] private int clicksToGain;
    
    private Transform target;
    
    /// Gestion du knockback
    private bool isKnockedBack = false;
    private float knockBackTimer = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }
    
    void Update()
    {
        Debug.Log(Time.timeScale);
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

    Coroutine lerpingTimeCoroutine = null;
        
    IEnumerator LerpingTime(float durationFirst,float secondDuration, float targetTimeFirst, float targetTimeSecond)
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
    }

    public void ApplyKnockBack(Vector2 direction, float force)
    {
        isKnockedBack = true;
        knockBackTimer = playerManager.knockBackDuration;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        lerpingTimeCoroutine = StartCoroutine(LerpingTime(0.1f, 0.4f, playerManager.knockBackSlowTime, 1f));
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
        ClickerManager.Instance.clicks += clicksToGain;
        ClickerManager.Instance.DisplayUpdate();
        Destroy(gameObject);
    }
}