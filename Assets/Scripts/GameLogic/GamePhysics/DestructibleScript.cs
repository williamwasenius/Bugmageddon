using UnityEngine;
using UnityEngine.UI;

public class DestructibleScript : MonoBehaviour, IDamageable
{
    // Public Variables
    public float CurrentHealth { get; set; }

    public GameObject hpContainer;
    public Image healthBar;
    public AudioPlayScript audioPlayScript;
    public float Armor => armor;
    public float MaxHealth => maxHealth;
    public float maxHealth = 100f;
    public float armor = 0f;

    public GameObject dustVFX;

    public bool NaturalObject = false;
    public bool fragile = false;


    void Start()
    {
        CurrentHealth = maxHealth;

        if (NaturalObject)
        {
            float randomScale = Random.Range(0.8f, 1.2f);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            float randomRotation = Random.Range(0, 360);
            transform.rotation = Quaternion.Euler(0, randomRotation, 0);
        }

        if (healthBar != null)
        {
            CurrentHealth = maxHealth;
        }

    }

    private void Update()
    {
        if (healthBar != null)
        {
            float normalizedHealth = Mathf.Clamp01(CurrentHealth / maxHealth);
            healthBar.fillAmount = normalizedHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (audioPlayScript != null)
        {
            audioPlayScript.Play(0, 1);
        }
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (NaturalObject || fragile)
        {
            Die();
        }
    }

    private void Die()
    {
        if (hpContainer != null)
        {
            hpContainer.SetActive(false);
        }
        if (dustVFX != null)
        {
            GameObject dust = Instantiate(dustVFX, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
