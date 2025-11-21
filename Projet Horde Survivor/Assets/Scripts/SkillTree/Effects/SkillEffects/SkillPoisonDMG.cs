using UnityEngine;

public class SkillPoisonDMG : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private int poisonDamage = 3;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        PlayerSkillHolderManager.Instance.poisonDamage = poisonDamage;
    }
}
