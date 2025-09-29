using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4Script : MonoBehaviour
{
    public GameObject player;

    public GameObject[] sensorPods;  
    public bool mission4Complete = false;

    public void FixedUpdate()
    {

        if (!mission4Complete && CheckAllPylonsCharged())
        {
            MissionComplete();
        }

        if (player == null)
        {
            Lose();
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

    private void MissionComplete()
    {
        Debug.Log("Congratulations! You completed the mission.");

        if (!SaveManager.Instance.mission4)
        {
            SaveManager.Instance.mission4 = true;
            SaveManager.Instance.SavePlayerData();
        }

        MissionTracker.Instance.MissionComplete();
        MissionTracker.Instance.MissionComplete();
        SceneManager.LoadScene("WeaponSelection");
    }
    private void Lose()
    {
        MissionTracker.Instance.MissionFailed();
        SceneManager.LoadScene("GameLoss");
    }

}