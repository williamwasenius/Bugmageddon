using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager Instance { get; private set; }

    // Public Variables
    public float destructibleRangeThreshold = 50f;
    public float enemyRangeThreshold = 100f;
    public float enemyInactiveTimeThreshold = 15f; 
    public List<GameObject> enemiesInScene = new List<GameObject>();

    public bool isLoading;

    // Private Variables
    private GameObject player;
    private List<GameObject> destructiblesInRange = new List<GameObject>();
    private Dictionary<GameObject, float> enemyInactiveTimers = new Dictionary<GameObject, float>();

    // Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        player = GameObject.FindWithTag("Player");

        AddDestructiblesAndEnemies();

        isLoading = true;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        player = GameObject.FindWithTag("Player");

        AddDestructiblesAndEnemies();
        UpdateEnemyList();

        isLoading = true;
    }

    private void Update()
    {
        ManageVisibility(destructiblesInRange, destructibleRangeThreshold, "Destructible");
        ManageVisibility(enemiesInScene, enemyRangeThreshold, "Enemy");
    }

    // Public Methods

    public void RegisterEnemy(GameObject enemy)
    {
        if (enemy != null && !enemiesInScene.Contains(enemy))
        {
            enemiesInScene.Add(enemy);
            enemyInactiveTimers[enemy] = 0f; 
        }
    }

    public void DeregisterEnemy(GameObject enemy)
    {
        if (enemy != null && enemiesInScene.Contains(enemy))
        {
            enemiesInScene.Remove(enemy);
            enemyInactiveTimers.Remove(enemy); 
        }
    }

    public void UpdateEnemyList()
    {
        enemiesInScene.RemoveAll(enemy => enemy == null || !enemy.activeInHierarchy);
    }

    // Private Methods

    private void AddDestructiblesAndEnemies()
    {
        GameObject[] destructibles = GameObject.FindGameObjectsWithTag("Destructible");
        foreach (GameObject obj in destructibles)
        {
            if (!destructiblesInRange.Contains(obj))
                destructiblesInRange.Add(obj);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            if (!enemiesInScene.Contains(obj))
            {
                enemiesInScene.Add(obj);
                enemyInactiveTimers[obj] = 0f; 
            }
        }
    }

    private void ManageVisibility(List<GameObject> objects, float rangeThreshold, string type)
    {
        if (player == null) return;

        foreach (GameObject obj in new List<GameObject>(objects)) 
        {
            if (obj != null)
            {
                float distance = Vector3.Distance(player.transform.position, obj.transform.position);

                if (distance > rangeThreshold)
                {
                    if (obj.activeInHierarchy)
                    {
                        obj.SetActive(false);
                        Debug.Log($"{type} {obj.name} is now unloaded.");
                    }

                    // Update inactive timer
                    if (type == "Enemy" && enemyInactiveTimers.ContainsKey(obj))
                    {
                        enemyInactiveTimers[obj] += Time.deltaTime;

                        if (enemyInactiveTimers[obj] >= enemyInactiveTimeThreshold)
                        {
                            Destroy(obj);
                            objects.Remove(obj);
                            enemyInactiveTimers.Remove(obj);
                            Debug.Log($"Enemy {obj.name} was inactive for too long and has been destroyed.");
                        }
                    }
                }
                else
                {
                    if (!obj.activeInHierarchy)
                    {
                        obj.SetActive(true);
                        Debug.Log($"{type} {obj.name} is now loaded.");
                    }

                    if (type == "Enemy" && enemyInactiveTimers.ContainsKey(obj))
                    {
                        enemyInactiveTimers[obj] = 0f;
                    }
                }
            }
        }
    }
}
