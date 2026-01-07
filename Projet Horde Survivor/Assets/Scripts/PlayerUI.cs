using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float slowHealth;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _levelTxt;

    void Start()
    {
        _slider.maxValue = PlayerManager.Instance.maxHealth;
        health = PlayerManager.Instance.currentHealth;
        slowHealth = health;
    }

    private void Update()
    {
        health = PlayerManager.Instance.currentHealth;

        slowHealth = Mathf.MoveTowards
        (
            slowHealth,
            health,
            5f * Time.fixedDeltaTime
        );
        
        UpdateHealthDisplay();
    }
    
    public void UpdateHealthDisplay()
    {
        _slider.value = slowHealth;
        _levelTxt.text = slowHealth.ToString("00.00") + "%";
    }
}
