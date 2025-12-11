using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPopup;
    public GameObject[] panel;
    public GameObject newGameWarningPanel;

    private GameManager gameManager;
    private MissionTracker missionTracker;

    public AudioMixer mixer;

    private void Start()
    {
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }

        SaveManager.Instance.LoadPlayerData();

        FindGameSystems();

        SaveManager.Instance.LoadPlayerData();
        AchievementManager.Instance.LoadAchievementData();
        mixer.SetFloat("MusicVol", Mathf.Log10(SaveManager.Instance.volumeSliderAmount) * 20);
        mixer.SetFloat("MusicVol", Mathf.Log10(SaveManager.Instance.musicSliderAmount) * 20);
        mixer.SetFloat("VFXVol", Mathf.Log10(SaveManager.Instance.vFXSliderAmount) * 20);
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

        //open warning panel
        Options(newGameWarningPanel);
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

    public void NewGameWarningYes()
    {
        SaveManager.Instance.DefaultPlayerData();
        SaveManager.Instance.SavePlayerData();


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
        Options(newGameWarningPanel);
    }
    public void NewGameWarningNo()
    {
        Options(newGameWarningPanel);
    }
}
