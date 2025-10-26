using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaytestingCanvas : MonoBehaviour
{
    public GameObject testingPanel;
    private MissionTracker missionTracker;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        FindTracker();
        testingPanel.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindTracker();
    }

    private void FindTracker()
    {
        if (missionTracker == null)
        {
            GameObject gm = GameObject.FindWithTag("GameManager");
            if (gm != null)
                missionTracker = gm.GetComponent<MissionTracker>();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            testingPanel.SetActive(!testingPanel.activeSelf);
        }
    }

    public void LoadMission1()
    {
        Debug.Log("Mission 1 selected");
        missionTracker.nextMission = "Mission1";
        SceneManager.LoadScene("WeaponSelection");
    }

    public void LoadMission2()
    {
        Debug.Log("Mission 2 selected");
        missionTracker.nextMission = "Mission2";
        SceneManager.LoadScene("WeaponSelection");
    }

    public void LoadMission3()
    {
        Debug.Log("Mission 3 selected");
        missionTracker.nextMission = "Mission3";
        SceneManager.LoadScene("WeaponSelection");
    }

    public void LoadMission4()
    {
        Debug.Log("Mission 4 selected");
        missionTracker.nextMission = "Mission4";
        SceneManager.LoadScene("WeaponSelection");
    }

    public void LoadMission5()
    {
        Debug.Log("Mission 5 selected");
        missionTracker.nextMission = "Mission5";
        SceneManager.LoadScene("WeaponSelection");
    }

}
