using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission5Script : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    private BossStateMachine bossState;

    public CanvasGroup winCanvas;
    public CanvasGroup failCanvas;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        bossState = boss.GetComponent<BossStateMachine>();

        MissionTracker.Instance.SetCurrentMission("Mission5");
    }

    private void Update()
    {
        if (bossState.isDefeated)
        {
            StartCoroutine(Win());
        }

        if (player == null)
        {
            Fail();
        }
    }

    private IEnumerator Win()
    {
        MissionScriptUniversalFunctions.CompleteMission("Mission5",null,this, false);
        
        foreach (GameObject enemy in EnemyEntitiesManagerScript.Instance.enemiesInScene)
        {
            EnemyCore enemyCore = enemy.GetComponent<EnemyCore>();
            if (!enemyCore.coreStats.isBoss)
            {
                enemyCore.Die();
            }
        }

        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("GameWin");

    }

    private void Fail()
    {
        MissionScriptUniversalFunctions.FailMission(failCanvas,this, true);
    }
}
