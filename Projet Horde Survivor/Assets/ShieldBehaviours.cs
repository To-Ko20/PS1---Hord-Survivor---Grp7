using System;
using UnityEngine;

public class ShieldBehaviours : MonoBehaviour
{
    [SerializeField] float cooldown;
    [SerializeField] float force;
    [SerializeField] SpriteRenderer spriteRenderer;
    private bool canDeflect = true;
    private float shieldTimer;

    private void Start()
    {
        cooldown = PlayerSkillHolderManager.Instance.shieldCooldown;
        force = PlayerSkillHolderManager.Instance.shieldForce;
        shieldTimer = cooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject enemy in EnemyManager.Instance.activeEnemies)
        {
            if (collision.transform == enemy.transform && canDeflect)
            {
                Deflect();
            }
        }
    }

    private void Deflect()
    {
        PlayerManager.Instance.Knockback(force);
        shieldTimer = cooldown;
        canDeflect = false;
        spriteRenderer.enabled = false;
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
