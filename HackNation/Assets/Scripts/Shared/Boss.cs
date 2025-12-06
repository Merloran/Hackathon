using UnityEngine;
using UnityEngine.UI;
using System; // Potrzebne do Action

public class Boss : MonoBehaviour
{
    public BossData data;
    public Slider slider;

    public event Action<Boss> OnDeath;
    public event Action OnHealthChanged;

    public void Initialize(BossData assignedData)
    {
        data = assignedData;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (slider != null && data != null)
        {
            slider.maxValue = data.maxHealth;
            slider.value = data.currentHealth;
        }
    }

    public void ApplyDamage(float damage)
    {
        if (data == null) return;

        data.currentHealth -= damage;

        UpdateUI();

        OnHealthChanged?.Invoke();

        if (data.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}