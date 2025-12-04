using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission1Script : MonoBehaviour
{
    public GameObject player;
    public int enemiesRemaining;
    public TextMeshProUGUI enemyCounter;

    public CanvasGroup winCanvas;
    public CanvasGroup failCanvas;

    private void Awake()
    {
        UpdateEnemyCount();
    }

    private void Start()
    {
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
        enemiesRemaining = EnemyEntitiesManagerScript.Instance.enemiesInScene.Count;
        enemiesRemaining += GameObject.FindGameObjectsWithTag("Spawner").Length;
    }

    private void Win()
    {
        MissionScriptUniversalFunctions.CompleteMission("Mission1",winCanvas,this, true);
    }

    private void Fail()
    {
        MissionScriptUniversalFunctions.FailMission(failCanvas,this, true);
    }
}
