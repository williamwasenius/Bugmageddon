using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private MissionTracker missionTracker;
    public GameObject missionUI;
    public GameObject missionInfoUI;
    public GameObject combatCanvas;
    public GameObject menuCanvas;
    public GameObject menu;
    public GameObject options;

    private bool missionUIOn = true;

    private void Start()
    {
        missionTracker = MissionTracker.Instance;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuCanvas == null)
            { return; }

            if (missionInfoUI.activeSelf)
            {
                missionInfoUI.SetActive(false);
                if (missionUIOn)
                {
                    missionUI.SetActive(true);
                }
            }

            else if (menuCanvas.activeSelf)
            {
                CloseMenu();
            }
            else if (Time.timeScale != 0)
            {
                OpenMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale != 0 && !missionInfoUI.activeInHierarchy)
        {
            if (menuCanvas == null)
            { return; }

            if (missionUI.activeSelf)
            {
                missionUIOn = false;
                missionUI.SetActive(false);
            }
            else
            {
                missionUIOn = true;
                missionUI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.O) && Time.timeScale != 0)
        {
            if (missionInfoUI == null)
            { return; }

            Cursor.visible = !Cursor.visible;
            MissionInfoPanel();
        }

    }

    public void MissionInfoPanel()
    {

        if (missionInfoUI.activeSelf)
        {
            missionInfoUI.SetActive(false);
            combatCanvas.SetActive(true);
            if (missionUIOn)
            {
                missionUI.SetActive(true);
            }
        }
        else
        {
            missionInfoUI.SetActive(true);
            combatCanvas.SetActive(false);
            missionUI.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        missionUI.SetActive(false);
        combatCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        combatCanvas.SetActive(true);
        if (missionUIOn)
        {
            missionUI.SetActive(true);
        }
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
    public void NextMission()
    {
        SceneManager.LoadScene("WeaponSelection");
    }

    public void RetryMission()
    {
        SceneManager.LoadScene("WeaponSelection");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        options.SetActive(true);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
