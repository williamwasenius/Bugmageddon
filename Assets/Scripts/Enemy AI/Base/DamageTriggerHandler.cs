using UnityEngine;

public class DamageTriggerHandler : MonoBehaviour
{
    private EnemyStateMachine EnemySM;
    public Collider trigger;
    public float triggerDamage = 0f;
    public bool singleInstance;
    public bool staticDamage;

    public void OnEnable()
    {
        if (trigger.enabled == false)
        {
            trigger.enabled = true;
        }
    }

    private void Start()
    {
        trigger = GetComponent<Collider>();

        if (!staticDamage)
        {
            EnemySM = GetComponentInParent<EnemyStateMachine>();

            if (EnemySM != null)
            {
                triggerDamage = EnemySM.enemyCS.meleeStats.damage;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled || other.isTrigger)
            return;

        IDamageable target = other.GetComponent<IDamageable>();
        if (target == null)
            return;

        if (EnemySM != null)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Objective"))
                return;
        }

        float damage = Mathf.Max(0f, triggerDamage - target.Armor);
        target.TakeDamage(damage);

        if (singleInstance)
        {
            trigger.enabled = false;
        }
    }

}
