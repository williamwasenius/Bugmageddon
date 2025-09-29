using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTracker : MonoBehaviour
{
    public static MissionTracker Instance;

    [Header("Mission state")]
    public string nextMission = "Mission1";   
    public string lastCompletedMission = "";    

    public bool lastMissionComplete;
    public bool lastMissionFailed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        nextMission = "Mission1";
    }

    public void MissionComplete(string beatenMission)
    {
        lastMissionComplete = true;
        lastMissionFailed = false;

        lastCompletedMission = beatenMission;

        switch (beatenMission)
        {
            case "Mission1": nextMission = "Mission2"; break;
            case "Mission2": nextMission = "Mission3"; break;
            case "Mission3": nextMission = "Mission4"; break;
            case "Mission4": nextMission = "Mission5"; break;
            case "Mission5":
                nextMission = "Mission1";
                SceneManager.LoadScene("GameWin"); 
                return;
        }
    }

    public void MissionFailed()
    {
        lastMissionFailed = true;
        lastMissionComplete = false;

        nextMission = "Mission1";
    }

    public string GetNextMissionScene()
    {
        return nextMission;
    }
}
