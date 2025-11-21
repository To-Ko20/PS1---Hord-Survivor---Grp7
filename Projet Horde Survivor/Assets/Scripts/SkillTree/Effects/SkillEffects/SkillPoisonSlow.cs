using UnityEngine;

public class SkillPoisonSlow : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private float slowForce = 0.5f;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        PlayerSkillHolderManager.Instance.hasSlowZone = true;
        PlayerSkillHolderManager.Instance.slowForce = slowForce;
    }
}
