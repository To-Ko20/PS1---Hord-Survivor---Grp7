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
                Shoot(PlayerSkillHolderManager.Instance.nbShootBullet);
            }
        }
    }

    private void Shoot(int nb)
    {
        
        float rotation = 360f / nb;
        for (int i = 0; i < nb; i++)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector3 dir = (targetingSystem.nearestEnemy.position -  transform.position).normalized;
            dir = Quaternion.Euler(0, 0, rotation*i) * dir;
            //Debug.Log(rotation*i);
            BulletMovement newBulletMovement = newBullet.GetComponent<BulletMovement>();
            newBulletMovement.bulletVector = dir;
            BulletManager.Instance.bulletList.Add(newBullet);
            newBulletMovement.collider2D.enabled = true;
        }
    }
}
