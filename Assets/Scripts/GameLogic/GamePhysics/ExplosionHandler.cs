using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionHandler : MonoBehaviour
{
    public float radius;
    public float damage;
    public float duration = 0.5f;

    public void Start()
    {
        Destroy(gameObject, duration);
    }

    public void TriggerExplosion()
    {
        LayerMask ignoredTrigger = LayerMask.NameToLayer("Trigger");

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits)
        {
            if (hit.isTrigger && hit.gameObject.layer == ignoredTrigger) continue;

            IDamageable dmg = hit.GetComponentInParent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage - dmg.Armor);
            }
        }
    }
}
