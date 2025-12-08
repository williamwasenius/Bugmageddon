using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPopup;
    public GameObject[] panel;

    private GameManager gameManager;
    private MissionTracker missionTracker;

    private void Start()
    {
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }

        SaveManager.Instance.LoadPlayerData();

        FindGameSystems();
    }

    private void FindGameSystems()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            GameObject gmObj = GameObject.FindWithTag("GameManager");
            if (gmObj != null)
            {
                gameManager = gmObj.GetComponent<GameManager>();
            }
        }

        if (gameManager != null)
        {
            missionTracker = gameManager.GetComponent<MissionTracker>();
        }

        if (missionTracker == null)
        {
            missionTracker = MissionTracker.Instance;
            if (missionTracker == null)
            {
                missionTracker = FindAnyObjectByType<MissionTracker>();
            }
        }
    }

    public void OpenCloseMenu(int panelNR)
    {
        panel[panelNR].SetActive(!panel[panelNR].activeInHierarchy);
    }

    public void StartNewGame()
    {
        FindGameSystems();

        SaveManager.Instance.tutorialMission = false;
        SaveManager.Instance.mission1 = false;

        if (!SaveManager.Instance.tutorialMission)
        {
            Options(tutorialPopup);
        }
        else
        {
            if (missionTracker != null)
            {
                missionTracker.ResetProgress(); 
                missionTracker.nextMission = "Mission1";
            }

            SceneManager.LoadScene("WeaponSelection");
        }
    }

    public void ContinueGame()
    {
        FindGameSystems();

        SceneManager.LoadScene("WeaponSelection");
    }

    public void Options(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void TutorialYes()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void TutorialNo()
    {
        SaveManager.Instance.tutorialMission = true;
        SaveManager.Instance.SavePlayerData();

        if (missionTracker != null)
        {
            missionTracker.ResetProgress();
            missionTracker.nextMission = "Mission1";
        }

        SceneManager.LoadScene("WeaponSelection");
    }
}
