using UnityEngine;

public class SkillRSKnockback : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private float force;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        PlayerSkillHolderManager.Instance.rsKnockbackForce = force;
    }
}
