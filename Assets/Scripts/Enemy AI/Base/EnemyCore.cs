using UnityEngine;
using UnityEngine.UI;

public class EnemyCore : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public EnemyCoreStatsSO coreStats;
    public EnemyMeleeStatsSO meleeStats;
    public EnemyRangedStatsSO rangedStats;
    public EnemyDetonatorStatsSO detonatorStats;
    public EnemyChargerStatsSO chargerStats;

    [Header("References")]
    public Canvas healthbarUI;
    public Image filler;
    public Animator animator;

    public float CurrentHealth { get; set; }
    public float MaxHealth => coreStats.maxHealth;
    public float Armor => coreStats.armor;

    private GameManager gameManager;

    public GameObject vfxPrefab;
    public GameObject vfxSpawnlocation;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        gameManager = GameManager.Instance;
        CurrentHealth = coreStats.maxHealth;
        gameManager.RegisterEnemy(gameObject);
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
        if (detonatorStats != null)
            Instantiate(detonatorStats.explosionPrefab, transform.position, Quaternion.identity);

        gameManager.DeregisterEnemy(gameObject);
        Instantiate(vfxPrefab, vfxSpawnlocation.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
