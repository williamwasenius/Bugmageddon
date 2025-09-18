using UnityEngine;

public class DestructibleScript : MonoBehaviour, IDamageable
{
    // Public Variables
    public float CurrentHealth { get; set; }
    public float Armor => armor;
    public float MaxHealth => maxHealth;
    public float maxHealth = 100f;
    public float armor = 0f;
    public bool NaturalObject = false;

    // Unity Methods

    private void start()
    {
        if (NaturalObject)
        {
            float randomScale = Random.Range(0.8f, 1.2f);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            float randomRotation = Random.Range(0, 360);
            transform.rotation = Quaternion.Euler(0, randomRotation, 0);
        }
    }

    void Start()
    {
        CurrentHealth = maxHealth;
    }

    // Public Methods

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    // Private Methods

    private void OnCollisionEnter(Collision collision)
    {
        if (NaturalObject)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
