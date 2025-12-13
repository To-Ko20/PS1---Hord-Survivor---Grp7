using UnityEngine;

public class SkillForceField : MonoBehaviour
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
        PlayerSkillHolderManager.Instance.hasForceField = true;
    }
}
