using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPopup;

    public void Start()
    {
        if (Cursor.visible == false)
        {  
            Cursor.visible = true; 
        }
        SaveManager.Instance.LoadPlayerData();
    }

    public void StartGame()
    {
        if (!SaveManager.Instance.tutorialMission)
        {
            Options(tutorialPopup);
        }
        else
        {
            SceneManager.LoadScene("WeaponSelection");
        }
    }

    public void Options(GameObject panel)
    {
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void TutorialYes()
    {
        SceneManager.LoadScene("WeaponSelection");
    }
    public void TutorialNo()
    {
        SaveManager.Instance.tutorialMission = true;
        SaveManager.Instance.SavePlayerData();
        SceneManager.LoadScene("WeaponSelection");
    }
}
