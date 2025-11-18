using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float playerSpeed = 1f;
    
    public bool canMove = true;
    private float t = 1f;
    private bool canApplyPoison = true;
    
    [SerializeField] private CircleCollider2D circleCollider2D;

    private Vector2 movement;
    
    public static PlayerController Instance;

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
    
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        circleCollider2D.radius = PlayerSkillHolderManager.Instance.magnetRadius;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * (playerSpeed * Time.fixedDeltaTime));
        }
        Countdown();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerSkillHolderManager.Instance.hasPoisonZone && canApplyPoison)
        {
            foreach (GameObject nmi in EnemyManager.Instance.activeEnemies)
            {
                if (collision.transform == nmi.transform)
                {
                    nmi.GetComponent<EnemyMovement>().EnemyTakeDamage(PlayerSkillHolderManager.Instance.poisonDamage);
                    canApplyPoison = false;
                }   
            }
        }
    }

    private void Countdown()
    {
        t -= Time.fixedDeltaTime;
        
        if (t <= 0)
        {
            t = 1;
            canApplyPoison = true;
        }
    }
}