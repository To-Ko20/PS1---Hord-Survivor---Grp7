using System;
using UnityEngine;

public class SkillDamageIncrease2 : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private float          damageIncrease    = 1f;
    [SerializeField] private float          maxHealthIncrease = 1f;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        BulletManager.Instance.bulletDamageBonus += damageIncrease;
        BulletManager.Instance.RecalculateDamage(2);
        
        PlayerManager.Instance.maxHealthBonus += maxHealthIncrease;
        PlayerManager.Instance.RecalculateMaxHealth();
    }
}