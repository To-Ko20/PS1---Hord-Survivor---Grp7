using UnityEngine;

public class PlayerSkillHolderManager : MonoBehaviour
{
    [Header("Data Magnet")]
    public bool hasMagnet = false;
    public bool hasPoisonZone = false;
    public float magnetRadius = 2f;
    public float magnetForce = 2f;
    [Space(10f)]

    [Header("Data Magnet")]
    public bool hasRotativeShield;
    public GameObject rotativeShield;
    public float poisonDamage;
    public bool hasSlowZone;
    public float slowForce;
    [Space(10f)]
    
    [Header("Shoot")]
    public int nbShootBullet = 1;
    
    public static PlayerSkillHolderManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateRotativeShield()
    {
        rotativeShield.SetActive(true);
    }
}
