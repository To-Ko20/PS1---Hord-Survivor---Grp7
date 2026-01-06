using System.Collections.Generic;
using UnityEngine;

public class PoisonZone : MonoBehaviour
{
    [SerializeField] private float t = 1f;
    
    public List<EnemyMovement> inZoneEnemies = new List<EnemyMovement>();
    
    [SerializeField] private Transform radiusTransform;
    [SerializeField] private SpriteRenderer radiusSpriteRenderer;
    
    void FixedUpdate()
    {
        if (PlayerSkillHolderManager.Instance.hasMagnet)
        {
            radiusSpriteRenderer.enabled = true;
        }
        float radius = (3.75f * PlayerSkillHolderManager.Instance.magnetRadius) / 2;
        radiusTransform.localScale = new Vector3(radius, radius, radius);
        if (PlayerSkillHolderManager.Instance.hasPoisonZone)
        {
            PoisonCountdown();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!PlayerSkillHolderManager.Instance.hasPoisonZone) return;
        if (!EnemyManager.Instance.enemyColliderInstanceIDs.Contains(other.GetInstanceID())) return;
        EnemyMovement nmi = other.gameObject.GetComponent<EnemyMovement>();
        inZoneEnemies.Add(nmi);
        if (PlayerSkillHolderManager.Instance.hasSlowZone)
        {
            nmi.hasToSlow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!PlayerSkillHolderManager.Instance.hasPoisonZone) return;
        if (!EnemyManager.Instance.enemyColliderInstanceIDs.Contains(other.GetInstanceID())) return;
        EnemyMovement nmi = other.gameObject.GetComponent<EnemyMovement>();
        inZoneEnemies.Remove(nmi);
        if (PlayerSkillHolderManager.Instance.hasSlowZone)
        {
            nmi.hasToSlow = false;
        }
    }

    private void ApplyPoison()
    {
        List<EnemyMovement> localInZoneEnemies = new List<EnemyMovement>(inZoneEnemies);

        foreach (EnemyMovement nmi in localInZoneEnemies)
        {
            if (nmi == null)
            {
                inZoneEnemies.Remove(nmi);
            }
            else
            {
                nmi.EnemyTakeDamage(PlayerSkillHolderManager.Instance.poisonDamage, "poison");
                Debug.Log("Send Poison Damage");  
            }
        }
    }
    
    private void PoisonCountdown()
    {
        t -= Time.fixedDeltaTime;
        
        if (t <= 0)
        {
            t = 1;
            ApplyPoison();
        }
    }
}
