using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float currentHealth;
    public int maxHealth;
    
    [SerializeField] private PlayerUI pUI;

    public static PlayerManager Instance;
    
    //Knockback parameters
    public GameObject knockbackZone;
    public int knockbackStrenght; 
    public float knockBackDuration = 1f;

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
        
        knockbackZone = GameObject.FindGameObjectWithTag("KnockbackZone");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
        
        //knockback
        float radius = knockbackZone.GetComponent<CircleCollider2D>().radius * knockbackZone.transform.localScale.x;

        foreach (var enemy in EnemyManager.Instance.activeEnemies)
        {
            float distance = Vector2.Distance(knockbackZone.transform.position, enemy.transform.position);

            if (distance <= radius)
            {
                Rigidbody2D rb = enemy.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (enemy.transform.position - knockbackZone.transform.position).normalized;
                    enemy.GetComponent<EnemyMovement>()?.ApplyKnockBack(direction, knockbackStrenght);
                }
            }
        }
    }
}