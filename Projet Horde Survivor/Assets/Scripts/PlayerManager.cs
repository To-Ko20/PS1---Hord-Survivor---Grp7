using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 10;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Debug.Log("Current Health: " + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
