using UnityEngine;

public class SkillRSDMG : MonoBehaviour
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
        PlayerSkillHolderManager.Instance.rsHasDamages = true;
        PlayerSkillHolderManager.Instance.spikeShield = true;
        PlayerSkillHolderManager.Instance.ActivateSpikeShield();
    }
}
