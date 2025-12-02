using System.Collections;
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

    private EnemyEntitiesManagerScript entityManagerScript;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        entityManagerScript = EnemyEntitiesManagerScript.Instance;

        CurrentHealth = coreStats.maxHealth;

        if (meleeStats && !damageTrigger)
        {
            damageTrigger = GetComponentInChildren<DamageTriggerHandler>();
            damageTrigger.triggerDamage = meleeStats.damage;
            damageTrigger.gameObject.SetActive(false);
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
        if (CurrentHealth <= 0) StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        if (bursterStats != null)
        {
            var explosion = Instantiate(bursterStats.explosionPrefab, transform.position, Quaternion.identity)
                .GetComponent<ExplosionHandler>();

            explosion.radius = bursterStats.explosionRadius;
            explosion.damage = bursterStats.explosionDamage;
            explosion.TriggerExplosion();
        }

        if (coreStats.deathClip != null)
        {
            yield return new WaitForSeconds(coreStats.deathDuration);
        }

        entityManagerScript.DeregisterEnemy(gameObject);
        Destroy(gameObject);
    }
}
