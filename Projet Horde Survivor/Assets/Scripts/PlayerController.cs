using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float playerSpeed = 1f;
    
    public bool canMove = true;
    private float t = 1f;
    private bool canApplyPoison = false;
    
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
    
    private void OnTriggerStay2D(Collider2D other)
    {
        List<GameObject> enemies = new List<GameObject>();
        if (!PlayerSkillHolderManager.Instance.hasPoisonZone) return;
        if (!EnemyManager.Instance.enemyColliderInstanceIDs.Contains(other.GetInstanceID())) return;
        enemies.Add(other.gameObject);
        
        if (canApplyPoison)
        {
            ApplyPoison(enemies);
            enemies.Clear();
        }
    }

    private void ApplyPoison(List<GameObject> enemies)
    {
        foreach (GameObject nmi in enemies)
        {
            nmi.GetComponent<EnemyMovement>().EnemyTakeDamage(PlayerSkillHolderManager.Instance.poisonDamage); 
            Debug.Log("poison damage");
        }
    }

    private void Countdown()
    {
        t -= Time.fixedDeltaTime;
        
        if (t <= 0)
        {
            t = 1;
            canApplyPoison = !canApplyPoison;
        }
    }
}