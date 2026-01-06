using System;
using UnityEngine;
using static PlayerSkillHolderManager;

public class ShieldBehaviours : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private bool canDeflect = true;
    [SerializeField] private float shieldTimer;
    private int shieldNb;

    private void Start()
    {
        force = Instance.shieldForce;
        shieldTimer = Instance.shieldCooldown;
        shieldNb = Instance.shieldNb;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject enemy in EnemyManager.Instance.activeEnemies)
        {
            if (other.transform == enemy.transform && canDeflect)
            {
                Deflect();
            }
        }
    }

    private void Deflect()
    {
        PlayerManager.Instance.Knockback(force);
        shieldTimer = Instance.shieldCooldown;
        canDeflect = false;
        if (shieldNb > 1)
        {
            shieldNb--;
        }
        else
        {
            spriteRenderer.enabled = false;
            shieldNb = Instance.shieldNb;
        }
        
    }
    
    void FixedUpdate()
    {
        if (!canDeflect)
        {
            Cooldown();
        }
    }

    private void Cooldown()
    {
        shieldTimer -= Time.fixedDeltaTime;
        if (shieldTimer <= 0)
        {
            canDeflect = true;
            spriteRenderer.enabled = true;
        }
    }
}
