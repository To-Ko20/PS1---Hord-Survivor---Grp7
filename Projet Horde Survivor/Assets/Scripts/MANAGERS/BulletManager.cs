using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage; //dégâts de base
    public float bulletDamageBonus = 0f; //stocke les modificateurs de bonus de dégâts
    public float bulletActualDamage; // dégâts appliqués
    public float bulletSize;
    
    public List<GameObject> bulletList;
    
    public static BulletManager Instance;
    
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

    public void RecalculateDamage()
    {
        bulletActualDamage = bulletDamage * (1f + bulletDamageBonus);
    }
}