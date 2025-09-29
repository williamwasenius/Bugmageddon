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
        MissionTracker.Instance.MissionComplete("Mission4");

        if (!SaveManager.Instance.mission4)
        {
            SaveManager.Instance.mission4 = true;
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