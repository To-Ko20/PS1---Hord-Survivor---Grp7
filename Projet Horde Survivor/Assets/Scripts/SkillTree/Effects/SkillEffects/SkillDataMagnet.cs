using System;
using UnityEngine;

public class SkillDataMagnet : MonoBehaviour
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
        PlayerSkillHolderManager.Instance.hasMagnet = true;
        PlayerSkillHolderManager.Instance.magnetRadius = 3f;
        PlayerSkillHolderManager.Instance.magnetForce = 4f;
    }
}
