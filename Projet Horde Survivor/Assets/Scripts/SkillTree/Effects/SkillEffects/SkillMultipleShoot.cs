using UnityEngine;

public class SkillMultipleShoot : MonoBehaviour
{
    [SerializeField] private SkillActivator sk;
    [SerializeField] private int nbBullet;
    
    public void Activate()
    {
        Effect();
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }

    private void Effect()
    {
        PlayerSkillHolderManager.Instance.nbShootBullet = nbBullet;
    }
}
