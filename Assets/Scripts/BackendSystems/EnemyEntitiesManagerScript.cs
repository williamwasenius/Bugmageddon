using UnityEngine;
using System.Collections.Generic;

public class EnemyEntitiesManagerScript : MonoBehaviour
{
    public static EnemyEntitiesManagerScript Instance { get; private set; }

    [Header("Distance Settings (per mission/area)")]
    public float destructibleRangeThreshold = 200f;
    public float enemyRangeThreshold = 200f;
    public float enemyInactiveTimeThreshold = 15f;

    [Header("Runtime Entity Tracking")]
    public List<GameObject> enemiesInScene = new List<GameObject>();
    private List<GameObject> destructiblesInRange = new List<GameObject>();
    private Dictionary<GameObject, float> enemyInactiveTimers = new Dictionary<GameObject, float>();

    private GameObject player;

    private void Awake()
    {
        Instance = this;
        player = GameObject.FindWithTag("Player");
        AddDestructiblesAndEnemies();
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            return;
        }

        ManageVisibility(destructiblesInRange, destructibleRangeThreshold, "Destructible");
        ManageVisibility(enemiesInScene, enemyRangeThreshold, "Enemy");
    }

    // ---------------- PUBLIC REGISTRATION ---------------- //

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

    // ---------------- ENTITY LOADING METHODS ---------------- //

    private void AddDestructiblesAndEnemies()
    {
        foreach (GameObject d in GameObject.FindGameObjectsWithTag("Destructible"))
            if (!destructiblesInRange.Contains(d)) destructiblesInRange.Add(d);

        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            RegisterEnemy(e);
    }

    private void ManageVisibility(List<GameObject> objects, float rangeThreshold, string type)
    {
        foreach (GameObject obj in new List<GameObject>(objects))
        {
            if (obj == null) continue;

            float dist = Vector3.Distance(player.transform.position, obj.transform.position);
            bool isFar = dist > rangeThreshold;
            EnemyCore enemyComponent = obj.GetComponent<EnemyCore>();

            // --------- FAR AWAY ---------
            if (isFar)
            {
                if (obj.activeInHierarchy) obj.SetActive(false);

                if (type == "Enemy" && enemyComponent != null && !enemyComponent.isPreplaced)
                {
                    if (enemyInactiveTimers.ContainsKey(obj))
                    {
                        enemyInactiveTimers[obj] += Time.deltaTime;

                        if (enemyInactiveTimers[obj] >= enemyInactiveTimeThreshold)
                        {
                            Destroy(obj);
                            objects.Remove(obj);
                            enemyInactiveTimers.Remove(obj);
                        }
                    }
                }
                continue;
            }

            // --------- NEARBY ---------
            if (!obj.activeInHierarchy) obj.SetActive(true);

            if (type == "Enemy" && enemyInactiveTimers.ContainsKey(obj))
                enemyInactiveTimers[obj] = 0f;
        }
    }
}
