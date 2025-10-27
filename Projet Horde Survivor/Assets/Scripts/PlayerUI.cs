using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float slowHealth;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _levelTxt;

    private float t = 0f;

    void Start()
    {
        _slider.maxValue = PlayerManager.Instance.maxHealth;
        health = PlayerManager.Instance.currentHealth;
        slowHealth = health;
    }

    void Update()
    {
        health = PlayerManager.Instance.currentHealth;
        
        if (slowHealth != health)
        {
            slowHealth = Mathf.Lerp(slowHealth, health, t);
            t += 0.05f * Time.deltaTime;
            UpdateHealthDisplay();
        }
    }
    
    public void UpdateHealthDisplay()
    {
        _slider.value = slowHealth;
        _levelTxt.text = slowHealth.ToString("00.00") + "%";
    }
}
