using UnityEngine;

public class SkillOneMoreShield : MonoBehaviour
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
        PlayerSkillHolderManager.Instance.shieldNb = 2;
        PlayerSkillHolderManager.Instance.shieldCooldown = 15;
    }
}
