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
        mission4Complete = true;
        SceneManager.LoadScene("Mission5");
    }
    private void Lose()
    {
        SceneManager.LoadScene("MainMenu");
    }

}