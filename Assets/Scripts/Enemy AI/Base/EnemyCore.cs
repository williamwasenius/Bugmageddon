using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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
    public DamageTriggerHandler chargeDamageTrigger;

    private bool hasExploded = false;

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
        if (chargerStats)
        {
            chargeDamageTrigger = GetComponent<DamageTriggerHandler>();
            chargeDamageTrigger.triggerDamage = chargerStats.chargeDamage;
            chargeDamageTrigger.gameObject.SetActive(false);
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
        if (CurrentHealth <= 0 && !coreStats.isBoss) StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        if (!hasExploded && bursterStats != null)
        {
            hasExploded = true;

            var explosion = Instantiate(bursterStats.explosionPrefab, transform.position, Quaternion.identity).GetComponent<ExplosionHandler>();

            explosion.radius = bursterStats.explosionRadius;
            explosion.damage = bursterStats.explosionDamage;
            explosion.TriggerExplosion();
        }

        /*if (coreStats.deathClip != null)
        {
            yield return new WaitForSeconds(coreStats.deathDuration);
        }*/

        yield return new WaitForSeconds(0);
        entityManagerScript.DeregisterEnemy(gameObject);
        Destroy(gameObject);
    }
}
