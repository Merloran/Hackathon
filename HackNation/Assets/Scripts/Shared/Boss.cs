using UnityEngine;
using UnityEngine.UI;
using System; // Potrzebne do Action

public class Boss : MonoBehaviour
{
    public BossData data;
    public Slider slider;
    public ParticleSystem damageParticles;
    public event Action<Boss> OnDeath;
    public event Action<bool> OnHealthChanged;

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

    public void ApplyDamage(float damage, bool isPlayer)
    {
        if (data == null) return;
        if (isPlayer)
        {
            damageParticles.Play();
        }
        data.currentHealth -= damage;

        UpdateUI();

        OnHealthChanged?.Invoke(gameObject);

        if (data.currentHealth <= 0)
        {
            Die(isPlayer);
        }
    }

    void Die(bool isPlayer)
    {
        if (isPlayer)
        {
            BossSceneData.scores += 20;
        }

        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}