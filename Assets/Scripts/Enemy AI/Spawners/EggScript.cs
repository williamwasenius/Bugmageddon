using UnityEngine;

public class EggScript : MonoBehaviour
{
    [Header("Egg Settings")]
    [Range(0f, 1f)] public float hatchChance = 0.4f;

    [Header("Enemy Spawning")]
    public GameObject[] enemyPool;

    [Header("Player object")]
    public GameObject player;

    private void OnEnable() => BossEvents.OnQueenRoar += ReleaseBugs;
    private void OnDisable() => BossEvents.OnQueenRoar -= ReleaseBugs;


    public void ReleaseBugs()
    {
        if (Random.value > hatchChance) return;
        Hatch();
    }

    private void Hatch()
    {
        EnemyEntitiesManagerScript entityManager = EnemyEntitiesManagerScript.Instance;

        if (entityManager.enemiesInScene.Count <= 100)
        {
            int randomIndex = Random.Range(0, enemyPool.Length);
            GameObject enemyPrefab = enemyPool[randomIndex];

            Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            GameObject spawned = Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity);

            spawned.GetComponent<EnemyCore>().isPreplaced = false;
            EnemyEntitiesManagerScript.Instance.RegisterEnemy(spawned);

            if (spawned != null)
            {
                enemyPrefab.GetComponent<EnemyCore>().isPreplaced = false;
                enemyPrefab.GetComponent<EnemyStateMachine>().chaseTarget = player.transform;
                EnemyEntitiesManagerScript.Instance.RegisterEnemy(spawned);
            }

            Debug.Log("Egg Hatched");
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
