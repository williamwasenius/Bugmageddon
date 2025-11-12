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
        SceneManager.LoadScene("MainMenu");
    }
}
