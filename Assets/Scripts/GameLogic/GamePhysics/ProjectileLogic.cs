using UnityEngine;
using System.Collections;

public class ProjectileLogic : MonoBehaviour
{
    public GameObject shooter;
    public AudioPlayScript audioPlayScript;
    public ProjectileStatsSO projectileStats;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (ProjectilePoolerScript.Instance != null)
        {
            StartCoroutine(DespawnAfterLifetime());
        }
        else
        {
            Destroy(gameObject, projectileStats.lifeTime);
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * projectileStats.speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleHit(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleHit(collision.gameObject);
    }

    private void HandleHit(GameObject hitObject)
    {
        if (shooter.CompareTag(hitObject.tag) || (shooter.CompareTag("Ally") && hitObject.CompareTag("Player")))
        {
            RemoveProjectile();
        }

        IDamageable target = hitObject.GetComponentInParent<IDamageable>();

        if (target != null)
        {
            if (hitObject.GetComponent<EnemyStateMachine>() != null)
            {
                EnemyStateMachine enemyStateMachine = hitObject.GetComponent<EnemyStateMachine>();
                enemyStateMachine.OnHit(shooter);
                if (audioPlayScript != null && shooter.CompareTag("Player"))
                { 
                    audioPlayScript.Play(0, 0.5f); 
                }
            }

            target.TakeDamage(projectileStats.damage * (1 - ((target.Armor - projectileStats.armorPierce) * 5) / 100));

            if (projectileStats.explosive)
                SpawnExplosion();

            if (projectileStats.pierce)
            {
                return;
            }

            RemoveProjectile();
            return;
        }

        if (projectileStats.explosive)
            SpawnExplosion();

        RemoveProjectile();
    }

    private void SpawnExplosion()
    {
        GameObject obj = Instantiate(projectileStats.explosionPrefab, transform.position, Quaternion.identity);

        ExplosionHandler handler = obj.GetComponent<ExplosionHandler>();
        handler.radius = projectileStats.explosionRadius;
        handler.damage = projectileStats.explosionDamage;

        handler.TriggerExplosion();
    }
    private void RemoveProjectile()
    {
        if (ProjectilePoolerScript.Instance != null)
        {
            ProjectilePoolerScript.Instance.Despawn(gameObject, projectileStats.projectilePrefab);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DespawnAfterLifetime()
    {
        yield return new WaitForSeconds(projectileStats.lifeTime);
        RemoveProjectile();
    }
}
