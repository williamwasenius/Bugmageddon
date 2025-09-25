using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;

    public void StartGame()
    {
        SceneManager.LoadScene("WeaponSelection");

        /*
        if (!SaveManager.Instance.tutorialMission)
        {
            //open tutorial popup
        }
        */
    }
    public void Options()
    {
        options.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
