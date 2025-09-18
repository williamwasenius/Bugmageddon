using UnityEngine;
using UnityEngine.UI;

public class PlayerSystems : MonoBehaviour, IDamageable
{
    public float CurrentHealth { get; set; }
    public float Armor => armor;
    public float MaxHealth => maxHealth;

    public float maxHealth = 100f;
    public float armor = 0f;

    public Image healthBar;

    void Start()
    {
        CurrentHealth = maxHealth;
    }
    public void Update()
    {
        float normalizedHealth = Mathf.Clamp01(CurrentHealth / maxHealth);
        healthBar.fillAmount = normalizedHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
