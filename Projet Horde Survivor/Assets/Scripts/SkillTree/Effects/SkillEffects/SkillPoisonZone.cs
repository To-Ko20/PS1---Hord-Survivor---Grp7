using UnityEngine;

public class SkillPoisonZone : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private int poisonDamage = 1;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        PlayerSkillHolderManager.Instance.hasPoisonZone = true;
        PlayerSkillHolderManager.Instance.poisonDamage = poisonDamage;
    }
}
