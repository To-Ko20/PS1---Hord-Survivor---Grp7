using System;
using UnityEngine;

public class SkillDamageIncrease1 : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        BulletManager.Instance.RecalculateDamage(2);
    }
}