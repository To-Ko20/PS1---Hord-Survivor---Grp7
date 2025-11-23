using UnityEngine;

public class PlayerSkillHolderManager : MonoBehaviour
{
    [Header("Data Magnet")]
    public bool hasMagnet = false;
    public bool hasPoisonZone = false;
    public float magnetRadius = 2f;
    public float magnetForce = 2f;
    public float poisonDamage;
    public bool hasSlowZone;
    public float slowForce;
    [Space(10f)]

    [Header("Rotative Shield")]
    public GameObject rotativeShield;
    public GameObject smallRotativeShield;
    public GameObject bigRotativeShield;
    public Animation rotativeShieldAnimation;
    public float rsKnockbackForce;
    public bool rsHasDamages;
    public int rsDamages;

    [Space(10f)]
    
    [Header("Shoot")]
    public int nbShootBullet = 1;
    public bool hasExplosiveShoot;
    public int explodeNb;
    public bool hasMine;
    public GameObject mine;
    public float mineRadius;
    public int mineDMG;
    public float mineRate;

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

    public void SpeedUpRotativeShield()
    {
        rotativeShieldAnimation["RotativeShieldTurn"].speed = 2.0f;
    }

    public void SizeUpRotativeShield()
    {
        smallRotativeShield.SetActive(false);
        bigRotativeShield.SetActive(true);
    }
}
