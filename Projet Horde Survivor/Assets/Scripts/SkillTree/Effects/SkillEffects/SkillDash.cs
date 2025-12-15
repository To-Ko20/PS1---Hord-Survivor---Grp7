using UnityEngine;

public class SkillDash : MonoBehaviour
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
        PlayerSkillHolderManager.Instance.hasDash = true;
        PlayerSkillHolderManager.Instance.dashNb++;
    }
}
