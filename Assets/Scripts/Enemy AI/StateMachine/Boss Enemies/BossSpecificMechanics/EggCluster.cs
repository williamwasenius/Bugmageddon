using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EggCluster : MonoBehaviour
{

    [Header("Egg Settings")]
    public int minSpawn = 2;
    public int maxSpawn = 6;
    [Range(0f, 1f)] public float hatchChance = 0.4f;

    [Header("Enemy Spawning")]
    public GameObject[] enemyPool;

    private void OnEnable() => BossEvents.OnQueenRoar += ReleaseBugs;
    private void OnDisable() => BossEvents.OnQueenRoar -= ReleaseBugs;

    public void ReleaseBugs()
    {
        if (Random.value > hatchChance) return;
        StartCoroutine(Hatch());
    }

    private IEnumerator Hatch()
    {
        int spawnCount = Random.Range(minSpawn, maxSpawn + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, enemyPool.Length);
            GameObject enemyPrefab = enemyPool[randomIndex];

            Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            GameObject spawned = Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity);

            spawned.GetComponent<EnemyCore>().coreStats.isPreplaced = false;
            EnemyEntitiesManagerScript.Instance.RegisterEnemy(spawned);

            if (enemyPrefab != null)
            {
                enemyPrefab.GetComponent<EnemyCore>().coreStats.isPreplaced = false;
                EnemyEntitiesManagerScript.Instance.RegisterEnemy(enemyPrefab);
            }

            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Egg Hatched");
        Destroy(gameObject);
    }
}
