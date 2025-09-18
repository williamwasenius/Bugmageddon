using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour, IDamageable
{
    // Public Variables
    public float CurrentHealth { get; set; }
    public float Armor => armor;
    public float MaxHealth => maxHealth;
    public float maxHealth = 100f;
    public float armor = 0f;

    public GameObject[] enemies;
    public Transform spawningLocation;
    public float spawnRadius = 100f;
    public float spawnLimit = 100f;
    public float spawningFrequency;

    public bool small;
    public bool medium;
    public bool large;

    public bool specializedSpawner;

    public Canvas HealthbarUI;
    public Image filler;

    // Private Variables
    private int enemiesRemaining = 0;
    private GameManager gameManager;
    public GameObject player;
    private float nextSpawnTime;

    // Unity Methods

    void Start()
    {

        gameManager = GameManager.Instance;
        CurrentHealth = maxHealth;

    }

    void FixedUpdate()
    {
        if (gameManager.isLoading)
        { return; }


        if (IsPlayerWithinRadius())
        {
            if (Time.time >= nextSpawnTime && specializedSpawner)
            {
                StartCoroutine(SpawnEnemy());
                nextSpawnTime = Time.time + spawningFrequency;
            }
            else if(Time.time >= nextSpawnTime && gameManager.enemiesInScene.Count < spawnLimit)
            {
                StartCoroutine(SpawnEnemy());
                nextSpawnTime = Time.time + spawningFrequency;
            }
        }
    }

    public void Update()
    {
        if (CurrentHealth == maxHealth)
        {
            HealthbarUI.enabled = false;
        }
        else
        {
            HealthbarUI.enabled = true;
        }

        float normalizedHealth = Mathf.Clamp01(CurrentHealth / maxHealth);
        filler.fillAmount = normalizedHealth;
    }

    // Private Methods

    private bool IsPlayerWithinRadius()
    {
        if (player == null) return false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= spawnRadius;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        GameObject enemyPrefab = enemies[randomIndex];

        GameObject spawnedEnemy = null;

        if (small)
        {
            spawnedEnemy = Instantiate(enemyPrefab, spawningLocation.position, spawningLocation.rotation);
        }
        else if (medium)
        {
            if (enemyPrefab.name.Contains("Small"))
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                    spawnedEnemy = Instantiate(enemyPrefab, spawningLocation.position + spawnOffset, spawningLocation.rotation);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (enemyPrefab.name.Contains("Medium"))
            {
                spawnedEnemy = Instantiate(enemyPrefab, spawningLocation.position, spawningLocation.rotation);
            }
        }
        else if (large)
        {
            if (enemyPrefab.name.Contains("Small"))
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                    spawnedEnemy = Instantiate(enemyPrefab, spawningLocation.position + spawnOffset, spawningLocation.rotation);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (enemyPrefab.name.Contains("Medium"))
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                    spawnedEnemy = Instantiate(enemyPrefab, spawningLocation.position + spawnOffset, spawningLocation.rotation);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (enemyPrefab.name.Contains("Large"))
            {
                spawnedEnemy = Instantiate(enemyPrefab, spawningLocation.position, spawningLocation.rotation);
            }
        }
    }
}
