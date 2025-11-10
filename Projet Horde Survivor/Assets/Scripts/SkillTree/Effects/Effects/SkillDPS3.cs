using System;
using UnityEngine;

public class SkillDPS3 : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private float healthRatio = 1f;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        if (PlayerManager.Instance.currentHealth / PlayerManager.Instance.maxHealth < healthRatio)
        {
            BulletManager.Instance.bulletDoubleDamage = true;
            BulletManager.Instance.RecalculateDamage();
        }
    }
}