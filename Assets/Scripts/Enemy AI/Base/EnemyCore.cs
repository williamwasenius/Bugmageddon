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
    private DamageTriggerHandler damageTrigger;

    public float CurrentHealth { get; set; }
    public float MaxHealth => coreStats.maxHealth;
    public float Armor => coreStats.armor;

    private EntityManagerScript entityManagerScript;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        GameObject managerObj = GameObject.FindGameObjectWithTag("MissionHandler");
        if (managerObj != null)
        {
            entityManagerScript = managerObj.GetComponent<EntityManagerScript>();
            if (entityManagerScript != null)
            {
                entityManagerScript.RegisterEnemy(gameObject);
            }
        }

        CurrentHealth = coreStats.maxHealth;

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

        entityManagerScript.DeregisterEnemy(gameObject);
        Destroy(gameObject);
    }
}
