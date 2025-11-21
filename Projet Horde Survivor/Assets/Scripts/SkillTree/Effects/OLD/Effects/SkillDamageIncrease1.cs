using System;
using UnityEngine;

public class SkillDamageIncrease1 : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private float damageIncrease = 1f;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        BulletManager.Instance.bulletDamageBonus += damageIncrease;
        BulletManager.Instance.RecalculateDamage();
    }
}