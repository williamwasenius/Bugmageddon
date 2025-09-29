using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2Script  : MonoBehaviour
{
    public GameObject player;

    public bool mission2Complete = false;
    public Mission2EndTrigger MissionFinished;

    private void FixedUpdate()
    {
        if (MissionFinished.EndTrigger == true)
        {
            MissionComplete();
        }

        if (player == null)
        {
            Lose();
        }
    }

    private void MissionComplete()
    {
        MissionTracker.Instance.MissionComplete("Mission2");

        if (!SaveManager.Instance.mission2)
        {
            SaveManager.Instance.mission2 = true;
            SaveManager.Instance.SavePlayerData();
        }

        SceneManager.LoadScene("WeaponSelection");
    }

    private void Lose()
    {
        MissionTracker.Instance.MissionFailed();
        SceneManager.LoadScene("GameLoss");
    }


}
