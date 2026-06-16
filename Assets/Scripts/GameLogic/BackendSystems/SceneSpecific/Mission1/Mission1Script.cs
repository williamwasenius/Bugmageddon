using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission1Script : MonoBehaviour
{
    public GameObject player;
    public int enemiesRemaining;
    public TextMeshProUGUI enemyCounter;
    private EnemyEntitiesManagerScript entityManagerScript;

    public CanvasGroup winCanvas;
    public CanvasGroup failCanvas;

    private void Awake()
    {

    }

    private void Start()
    {
        entityManagerScript = EnemyEntitiesManagerScript.Instance;
        UpdateEnemyCount();
        player = GameObject.FindWithTag("Player");
        MissionTracker.Instance.SetCurrentMission("Mission1");
    }

    private void Update()
    {
        enemyCounter.text = enemiesRemaining.ToString();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            Fail();
            return;
        }

        UpdateEnemyCount();

        if (enemiesRemaining <= 0)
            Win();
    }

    private void UpdateEnemyCount()
    {
        enemiesRemaining = entityManagerScript.enemiesInScene.Count + entityManagerScript.spawnersInScene.Count;
    }

    private void Win()
    {
        AchievementManager.Instance.GetAchievement("mission1Achievement");
        MissionScriptUniversalFunctions.CompleteMission("Mission1",winCanvas,this, true);
    }

    private void Fail()
    {
        MissionScriptUniversalFunctions.FailMission(failCanvas,this, true);
    }
}
