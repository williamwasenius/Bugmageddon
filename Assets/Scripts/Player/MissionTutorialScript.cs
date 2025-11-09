using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTutorialScript : MonoBehaviour
{
    public GameObject player;
    public bool tutorialComplete = false;
    public SphereCollider endPointTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
