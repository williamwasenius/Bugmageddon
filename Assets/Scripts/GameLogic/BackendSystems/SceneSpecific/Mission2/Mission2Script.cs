using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2Script : MonoBehaviour
{
    public GameObject player;
    public Mission2EndTrigger MissionFinished;

    public CanvasGroup winCanvas;
    public CanvasGroup failCanvas;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        MissionTracker.Instance.SetCurrentMission("Mission2");
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            Fail();
            return;
        }

        if (MissionFinished.EndTrigger)
            Win();
    }

    private void Win()
    {
        AchievementManager.Instance.GetAchievement("mission2Achievement");
        MissionScriptUniversalFunctions.CompleteMission("Mission2",winCanvas,this,true);
    }

    private void Fail()
    {
        MissionScriptUniversalFunctions.FailMission(failCanvas,this,true);
    }
}
