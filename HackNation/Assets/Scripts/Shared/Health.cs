using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public BossData data;
    public Slider slider;

    void Start()
    {
        slider.maxValue = data.maxHealth;
    }

    void Update()
    {
        slider.value = data.currentHealth;
    }

    public void ApplyDamage(float damage)
    {
        data.currentHealth -= damage;
    }
}
