using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTutorialScript : MonoBehaviour
{
    public GameObject player;
    public bool tutorialComplete = false;

    private void Awake()
    {
    }
    public void Update()
    {

    }

    private void FixedUpdate()
    {

        if (player == null)
        {
            Lose();
        }

        {
            Debug.Log("Mission Complete! All enemies are defeated.");
            // MissionComplete();
        }
    }

    private void MissionComplete()
    {
        Debug.Log("Congratulations! You completed the mission.");
        tutorialComplete = true;
        SceneManager.LoadScene("Mission2");
    }
    private void Lose()
    {
        SceneManager.LoadScene("GameLoss");
    }
}
