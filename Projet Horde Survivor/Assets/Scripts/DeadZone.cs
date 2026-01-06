using System;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathBoxCollider;
    [SerializeField] private float damage;
    
    [SerializeField] private bool inZone = false;
    private float t = 0f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position == deathBoxCollider.transform.position)
        {
            inZone = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.position == deathBoxCollider.transform.position)
        {
            inZone = false;
        }
    }

    private void FixedUpdate()
    {
        Timer();
    }

    private void Timer()
    {
        t -= Time.fixedDeltaTime;

        if (t <= 0f)
        {
            t = 1f;
            if (inZone)
            {
                player.GetComponent<PlayerManager>().TakeDamage(damage);
            }
        }
    }
}
