using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float currentHealth;
    public int maxHealth;
    
    [SerializeField] private PlayerUI pUI;

    public static PlayerManager Instance;

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
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}