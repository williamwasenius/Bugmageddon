using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    public bool isBoss = false;

    public float CurrentHealth { get; set; }
    public float Armor => armor;
    public float MaxHealth => maxHealth;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float armor = 0f;
    public float meleeDamage = 10f;
    public float attackSpeed = 1f;

    [Header("UI")]
    public Canvas HealthbarUI;
    public Image filler;

    [Header("References")]
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.RegisterEnemy(gameObject);
        CurrentHealth = maxHealth;
    }

    void Update()
    {
        HealthbarUI.enabled = !(CurrentHealth == maxHealth && !isBoss);
        filler.fillAmount = Mathf.Clamp01(CurrentHealth / maxHealth);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameManager.DeregisterEnemy(gameObject);
        Destroy(gameObject);
    }
}
