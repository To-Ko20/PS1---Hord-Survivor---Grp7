using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 1f;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
    }
    
    void Update()
    {
        rb.linearVelocity = (target.position - transform.position).normalized * speed; //déplace l'ennemi vers le joueur
    }
}