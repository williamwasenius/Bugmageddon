using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission5Script : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;

    public bool mission5complete = false;

    private void Update()
    {
        if (boss == null)
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
        mission5complete = true;
        SceneManager.LoadScene("GameWin");
    }
    private void Lose()
    {
        SceneManager.LoadScene("GameLoss");
    }
}
