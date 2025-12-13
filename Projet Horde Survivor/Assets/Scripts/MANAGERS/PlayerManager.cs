using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public float maxHealthBonus = 1f;
    
    [SerializeField] private PlayerUI pUI;

    public static PlayerManager Instance;
    
    //Knockback parameters
    public GameObject knockbackZone;
    public int knockbackStrenght; 
    public float knockBackDuration = 1f;

    private float radius;
    private bool canForceField = true;
    private float _t;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
        radius = knockbackZone.GetComponent<CircleCollider2D>().radius * knockbackZone.transform.localScale.x;
        
        knockbackZone = GameObject.FindGameObjectWithTag("KnockbackZone");
        RecalculateMaxHealth(); 
    }

    public void RecalculateMaxHealth()
    {
        maxHealth *= maxHealthBonus;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
        
        Knockback(1);
    }

    public void Knockback(float force)
    {
        foreach (var enemy in EnemyManager.Instance.activeEnemies)
        {
            float distance = Vector2.Distance(knockbackZone.transform.position, enemy.transform.position);

            if (PlayerSkillHolderManager.Instance.hasForceField && canForceField)
            {
                if (distance <= radius*1.5f)
                {
                    Rigidbody2D rb = enemy.gameObject.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 direction = (enemy.transform.position - knockbackZone.transform.position).normalized;
                        enemy.GetComponent<EnemyMovement>()?.ApplyKnockBack(direction, knockbackStrenght*force*1.5f);
                        canForceField =  false;
                    }
                }
            }
            else
            {
                if (distance <= radius)
                {
                    Rigidbody2D rb = enemy.gameObject.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 direction = (enemy.transform.position - knockbackZone.transform.position).normalized;
                        enemy.GetComponent<EnemyMovement>()?.ApplyKnockBack(direction, knockbackStrenght*force);  
                    }
                } 
            }
        }
    }

    private void FixedUpdate()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (canForceField) return;
        _t += Time.fixedDeltaTime;
        if (_t <= 30)
        {
            _t = 30;
            canForceField = true;
        }
    }
}