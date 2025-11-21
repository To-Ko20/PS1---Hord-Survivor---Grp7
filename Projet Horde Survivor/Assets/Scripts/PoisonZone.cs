using System.Collections.Generic;
using UnityEngine;

public class PoisonZone : MonoBehaviour
{
    [SerializeField] private float t = 1f;
    
    public List<EnemyMovement> inZoneEnemies = new List<EnemyMovement>();
    
    [SerializeField] private CircleCollider2D circleCollider2D;
    
    void FixedUpdate()
    {
        circleCollider2D.radius = PlayerSkillHolderManager.Instance.magnetRadius;
        if (PlayerSkillHolderManager.Instance.hasPoisonZone)
        {
            PoisonCountdown();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!PlayerSkillHolderManager.Instance.hasPoisonZone) return;
        if (!EnemyManager.Instance.enemyColliderInstanceIDs.Contains(other.GetInstanceID())) return;
        inZoneEnemies.Add(other.gameObject.GetComponent<EnemyMovement>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!PlayerSkillHolderManager.Instance.hasPoisonZone) return;
        if (!EnemyManager.Instance.enemyColliderInstanceIDs.Contains(other.GetInstanceID())) return;
        inZoneEnemies.Remove(other.GetComponent<EnemyMovement>());
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
                nmi.EnemyTakeDamage(PlayerSkillHolderManager.Instance.poisonDamage);
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
