using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    public bool isBoss = false;


    public float CurrentHealth { get; set; }
    public float Armor => armor;
    public float MaxHealth => maxHealth;

    public float maxHealth = 100f;
    public float armor = 0f;

    public float meleeDamage = 10f;
    public float attackSpeed = 1f;
    private float meleeCooldown = 0f;

    public float despawnRadius = 200f;

    public GameManager gameManager;

    public Canvas HealthbarUI;
    public Image filler;

    void Start()
    {
        gameManager = GameManager.Instance; 
        gameManager.RegisterEnemy(gameObject);
        CurrentHealth = maxHealth;
    }

    public void Update()
    {
        if (CurrentHealth == maxHealth && !isBoss)
        {
            HealthbarUI.enabled = false;
        }
        else
        {
            HealthbarUI.enabled = true;
        }

        float normalizedHealth = Mathf.Clamp01(CurrentHealth / maxHealth);
        filler.fillAmount = normalizedHealth;
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

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable targetDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (targetDamageable != null)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            if (Time.time >= enemy.meleeCooldown && collision.gameObject.CompareTag("Player"))
            {
                PlayerSystems player = collision.gameObject.GetComponent<PlayerSystems>();
                targetDamageable.TakeDamage(enemy.meleeDamage - player.Armor);
                enemy.meleeCooldown = Time.time + attackSpeed;
            }
            else if (Time.time >= enemy.meleeCooldown && (collision.gameObject.CompareTag("Objective") || collision.gameObject.CompareTag("Destructible")))
            {
                DestructibleScript destructible = collision.gameObject.GetComponent<DestructibleScript>();
                targetDamageable.TakeDamage(enemy.meleeDamage - destructible.Armor);
                enemy.meleeCooldown = Time.time + attackSpeed;
            }
        }
    }
}