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
        Debug.Log("Congratulations! You completed the mission.");
        mission2Complete = true;
        SceneManager.LoadScene("Mission4");
    }
    private void Lose()
    {
        SceneManager.LoadScene("GameLoss");
    }


}
