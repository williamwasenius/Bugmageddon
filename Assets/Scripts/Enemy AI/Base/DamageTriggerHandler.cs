using UnityEngine;

public class DamageTriggerHandler : MonoBehaviour
{
    private EnemyStateMachine enemy;
    public float triggerDamage = 0f;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyStateMachine>();

        if (enemy != null ) 
        {
            triggerDamage = enemy.enemyCoreScript.meleeDamage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled || other.isTrigger)
            return;

        IDamageable target = other.GetComponent<IDamageable>();
        if (target == null)
            return;

        if (enemy != null)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Objective"))
                return;
        }

        float damage = Mathf.Max(0f, triggerDamage - target.Armor);
        target.TakeDamage(damage);
    }

}
