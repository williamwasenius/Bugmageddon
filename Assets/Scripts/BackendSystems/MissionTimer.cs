using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTimer : MonoBehaviour
{
    public float elapsedTime = 0f;
    public TextMeshProUGUI timerUI;
    public GameManager manager;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (timerUI == null || manager == null)
            return;

        if (manager.isLoading)
        {
            timerUI.gameObject.SetActive(false);
        }
        else
        {
            timerUI.gameObject.SetActive(true);
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 10) % 10);

            timerUI.text = $"{minutes:00}.{seconds:00}.{milliseconds:0}";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MissionTimer");
        timerUI = obj ? obj.GetComponent<TextMeshProUGUI>() : null;

        if (scene.name == "MainMenu" && timerUI != null)
        {
            elapsedTime = 0f;
            timerUI.text = "00.00.0";
        }
    }
}
