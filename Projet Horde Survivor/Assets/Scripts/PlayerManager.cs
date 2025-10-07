using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Debug.Log(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
