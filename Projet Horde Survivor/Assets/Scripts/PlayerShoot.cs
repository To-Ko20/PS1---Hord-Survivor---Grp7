using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] TargetingSystem targetingSystem;
    
    public float countdownToShoot;
    private float remainingSeconds;
    
    public static PlayerShoot Instance;

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
            if (targetingSystem.nearestEnemy != null)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<BulletMovement>().bulletVector = (targetingSystem.nearestEnemy.position -  transform.position).normalized;
        BulletManager.Instance.bulletList.Add(newBullet);
    }
}
