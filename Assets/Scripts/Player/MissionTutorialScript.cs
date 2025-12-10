using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTutorialScript : MonoBehaviour
{
    public GameObject player;
    public bool tutorialComplete = false;
    public Mission2EndTrigger MissionFinished;

    private void FixedUpdate()
    {
        if (MissionFinished.EndTrigger == true)
        {
            MissionComplete();
        }
    }

    private void MissionComplete()
    {
        tutorialComplete = true;
        SaveManager.Instance.SavePlayerData();
        AchievementManager.Instance.GetAchievement("tutorialAchievement");
        SceneManager.LoadScene("WeaponSelection");
    }
}
