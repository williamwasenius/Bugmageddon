using UnityEngine;
using UnityEngine.UI;

public class EnemyCore : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public EnemyCoreStatsSO coreStats;
    public EnemyMeleeStatsSO meleeStats;
    public EnemyRangedStatsSO rangedStats;
    public EnemyDetonatorStatsSO bursterStats;
    public EnemyChargerStatsSO chargerStats;

    [Header("References")]
    public Canvas healthbarUI;
    public Image filler;
    public Animator animator;
    public DamageTriggerHandler damageTrigger;

    public float CurrentHealth { get; set; }
    public float MaxHealth => coreStats.maxHealth;
    public float Armor => coreStats.armor;

    private GameManager gameManager;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        gameManager = GameManager.Instance;
        CurrentHealth = coreStats.maxHealth;
        gameManager.RegisterEnemy(gameObject);

        if (meleeStats && !damageTrigger)
        {
            damageTrigger = GetComponentInChildren<DamageTriggerHandler>();
            damageTrigger.triggerDamage = meleeStats.damage;
        }
    }

    private void Update()
    {
        if (healthbarUI != null)
        {
            healthbarUI.enabled = !(CurrentHealth == MaxHealth);
            filler.fillAmount = Mathf.Clamp01(CurrentHealth / MaxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) Die();
    }

    public void Die()
    {
        if (bursterStats != null)
            Instantiate(bursterStats.explosionPrefab, transform.position, Quaternion.identity);

        gameManager.DeregisterEnemy(gameObject);
        Destroy(gameObject);
    }
}
