using System;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private EnemyMovement enemyMovement;
    
    [SerializeField] private float fireRate;
    private float timeToFire;
    
    [SerializeField] private float distanceToStop;
    [SerializeField] private float distanceToShoot;

    void Start()
    {
        target        = GameObject.FindGameObjectWithTag("Player");
        enemyMovement = GetComponent<EnemyMovement>();
        timeToFire    = fireRate;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= distanceToStop)
        {
            Shoot();
        }
        else if (Vector2.Distance(transform.position, target.transform.position) >= distanceToShoot)
        {
            enemyMovement.canMove = true;
        }
    }

    void Shoot()
    {
        enemyMovement.canMove = false;
        timeToFire -= Time.deltaTime;

        if (timeToFire <= 0)
        {
            Debug.Log("Shoot");
            timeToFire = fireRate;
        }
    }
}