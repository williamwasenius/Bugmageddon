using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTracker : MonoBehaviour
{
    public static MissionTracker Instance;

    public int currentMission = 1;
    public int maxMissions = 5;

    public bool lastMissionComplete = false;
    public bool lastMissionFailed = false;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MissionComplete()
    {
        lastMissionComplete = true;
        lastMissionFailed = false;

        // Advance mission (but don’t exceed max)
        if (currentMission < maxMissions)
            currentMission++;
        else if (currentMission == maxMissions)
        {
            currentMission = 1;
            SceneManager.LoadScene("GameComplete");
        }
    }

    public void MissionFailed()
    {
        lastMissionFailed = true;
        lastMissionComplete = false;

        // Reset mission back to 1 on failure
        currentMission = 1;
    }

    public string GetCurrentMissionScene()
    {
        return "Mission" + currentMission;
    }
}
