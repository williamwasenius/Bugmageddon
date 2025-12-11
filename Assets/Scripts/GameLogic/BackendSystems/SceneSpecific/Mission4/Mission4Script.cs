using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4Script : MonoBehaviour
{
    public static Mission4Script Instance { get; set; }

    public GameObject player;

    public CanvasGroup winCanvas;
    public CanvasGroup failCanvas;

    public GameObject[] sensorPods;  
    public bool mission4Complete = false;

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
        MissionTracker.Instance.SetCurrentMission("Mission4");
    }
    public void FixedUpdate()
    {

        if (!mission4Complete && CheckAllPylonsCharged())
        {
            Win();
        }

        if (player == null)
        {
            Fail();
        }

    }

    private bool CheckAllPylonsCharged()
    {
        foreach (GameObject pod in sensorPods)
        {
            PylonCharge pylonCharge = pod.GetComponent<PylonCharge>();
            if (pylonCharge == null || !pylonCharge.charged)
            {
                return false;
            }
        }

        return true;
    }

    private void Win()
    {
        AchievementManager.Instance.GetAchievement("mission4Achievement");
        MissionScriptUniversalFunctions.CompleteMission("Mission4",winCanvas,this, true);
    }

    private void Fail()
    {
        MissionScriptUniversalFunctions.FailMission(failCanvas,this, true);
    }

}