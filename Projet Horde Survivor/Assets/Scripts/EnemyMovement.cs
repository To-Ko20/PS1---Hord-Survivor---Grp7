using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float attackDamage = 1f;
    
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
    }
    
    void Update()
    {
        rb.linearVelocity = (target.position - transform.position).normalized * speed; //déplace l'ennemi vers le joueur
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerManager.instance.CompareTag("Player"))
        {
            PlayerManager.instance.TakeDamage(attackDamage);
        }
    }
}