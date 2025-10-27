using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] TargetingSystem targetingSystem;
    
    [SerializeField] private float countdownToShoot;
    private float remainingSeconds;

    void Start()
    {
        remainingSeconds = countdownToShoot;
    }
    
    void FixedUpdate()
    {
        Countdown();
    }

    private void Countdown()
    {
        remainingSeconds -= Time.fixedDeltaTime;
        
        if (remainingSeconds <= 0)
        {
            remainingSeconds = countdownToShoot;
            if (EnemyManager.Instance.activeEnemies.Count != 0)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<BulletMovement>().bulletVector = targetingSystem.nearestEnemy.position -  transform.position;
        BulletManager.Instance.bulletList.Add(newBullet);
    }
}
