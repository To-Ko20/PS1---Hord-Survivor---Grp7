using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletDamage;
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
}
