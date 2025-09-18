using Unity.VisualScripting;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{

    public float speed = 100f;
    public float damage = 20;
    private float currentDamage;
    public float amplifiedDamage;
    public float duration = 5;

    public bool isExplosive = false;
    public bool isPierce = false;

    public float explosionRadius = 2;

    public GameObject explosionPrefab;
    private PlayerController playerController;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        Rigidbody rb = GetComponent<Rigidbody>();
        SetDamage();
        Destroy(gameObject, duration);
    }

    void FixedUpdate()
    {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("impact");
        if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Destructible"))
        {
            Destroy(gameObject);
        }

        if (isExplosive)
        {
            Explode();
        }
        else
        {
            IDamageable targetDamageable = other.gameObject.GetComponent<IDamageable>();
            if (targetDamageable != null)
            {
                targetDamageable.TakeDamage(damage - targetDamageable.Armor);
            }
        }

        if (isPierce == false)
        {
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.isTrigger)
                continue;

            IDamageable targetDamageable = hitCollider.GetComponent<IDamageable>();
            if (targetDamageable != null)
            {
                targetDamageable.TakeDamage(currentDamage - targetDamageable.Armor);
            }
        }

    }

    private void SetDamage()
    {
        if (playerController.abilityActive == true)
        {
            Debug.Log("Damage amplified");
            damage = amplifiedDamage;
        }
        else
        {
            currentDamage = damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (isExplosive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}