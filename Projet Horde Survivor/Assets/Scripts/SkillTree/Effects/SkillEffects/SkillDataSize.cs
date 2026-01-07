using UnityEngine;

public class SkillDataSize : MonoBehaviour
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
        PlayerSkillHolderManager.Instance.magnetRadius = 4.5f;
        PlayerSkillHolderManager.Instance.magnetForce = 4f;
    }
}
