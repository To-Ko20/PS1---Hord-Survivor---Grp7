using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public float maxHealthBonus = 1f;
    
    [SerializeField] private PlayerUI pUI;
    [SerializeField] private GameObject glitchTrigger;
    
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material glitchMaterial;
    [SerializeField] private List<Renderer> rendererList = new List<Renderer>();

    public static PlayerManager Instance;
    
    //Knockback parameters
    public GameObject knockbackZone;
    public int knockbackStrenght; 
    public float knockBackDuration = 1f;

    private float radius;
    private bool canForceField = true;
    private float _t;
    private float _tRegen;

    private bool canTakeDamages = true;

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
    }

    private void Update()
    {
        RecalculateMaxHealth();
        if (PlayerSkillHolderManager.Instance.hasBerserkLife)
        {
            CalculateBerserkLife();
        }
    }

    private void CalculateBerserkLife()
    {
        if (currentHealth < (maxHealth * 0.2))
        {
            BulletManager.Instance.RecalculateDamage(2);
        }
        else
        {
            BulletManager.Instance.RecalculateDamage(1); 
        }
        
        //TODO feedbacks
    }

    public void RecalculateMaxHealth()
    {
        maxHealth *= maxHealthBonus;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void TakeDamage(float amount)
    {
        if (canTakeDamages)
        {
            canTakeDamages = false;
            currentHealth -= amount;
        
            if (currentHealth <= 0)
            {
                GameManager.Instance.GameOver();
            }
        
            StartCoroutine(DmgAnimation());
            Knockback(1);
            TriggerGlitch();  
        }
    }
    
    public void CursedDamages(float amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    IEnumerator DmgAnimation()
    {
        yield return new WaitForSeconds(0.0625f);
        rendererList[0].material = glitchMaterial;
        rendererList[1].material = glitchMaterial;
        rendererList[2].material = glitchMaterial;
        rendererList[3].material = glitchMaterial;
        rendererList[4].material = glitchMaterial;
        rendererList[5].material = glitchMaterial;
        rendererList[6].material = glitchMaterial;
        rendererList[7].material = glitchMaterial;
        rendererList[8].material = glitchMaterial;
        
        yield return new WaitForSeconds(0.0625f);
        rendererList[0].material = normalMaterial;
        rendererList[1].material = normalMaterial;
        rendererList[2].material = normalMaterial;
        rendererList[3].material = normalMaterial;
        rendererList[4].material = normalMaterial;
        rendererList[5].material = normalMaterial;
        rendererList[6].material = normalMaterial;
        rendererList[7].material = normalMaterial;
        rendererList[8].material = normalMaterial;
        
        yield return new WaitForSeconds(0.0625f);
        rendererList[0].material = glitchMaterial;
        rendererList[1].material = glitchMaterial;
        rendererList[2].material = glitchMaterial;
        rendererList[3].material = glitchMaterial;
        rendererList[4].material = glitchMaterial;
        rendererList[5].material = glitchMaterial;
        rendererList[6].material = glitchMaterial;
        rendererList[7].material = glitchMaterial;
        rendererList[8].material = glitchMaterial;
        
        yield return new WaitForSeconds(0.0625f);
        rendererList[0].material = normalMaterial;
        rendererList[1].material = normalMaterial;
        rendererList[2].material = normalMaterial;
        rendererList[3].material = normalMaterial;
        rendererList[4].material = normalMaterial;
        rendererList[5].material = normalMaterial;
        rendererList[6].material = normalMaterial;
        rendererList[7].material = normalMaterial;
        rendererList[8].material = normalMaterial;
        
        canTakeDamages = true;
    }

    public void GainLife(float amount)
    {
        currentHealth += amount;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Knockback(float force)
    {
        foreach (var enemy in EnemyManager.Instance.activeEnemies)
        {
            float distance = Vector2.Distance(knockbackZone.transform.position, enemy.transform.position);

            if (PlayerSkillHolderManager.Instance.hasForceField && canForceField)
            {
                if (maxHealth - currentHealth <= maxHealth * 0.15f)
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
    
    private void TriggerGlitch()
    {
        glitchTrigger.SetActive(true);
        StartCoroutine(WaitForSecondsBeforeEndSfx(0.5f));
    }

    IEnumerator WaitForSecondsBeforeEndSfx(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        glitchTrigger.SetActive(false);
    }

    private void FixedUpdate()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (PlayerSkillHolderManager.Instance.hasRegen)
        {
            _tRegen -= Time.fixedDeltaTime;
            if (_tRegen <= 0)
            {
                _tRegen = 3;
                GainLife(1);
            }
        }
        
        if (canForceField) return;
        _t += Time.fixedDeltaTime;
        if (_t <= 60)
        {
            _t = 60;
            canForceField = true;
        }
    }
}