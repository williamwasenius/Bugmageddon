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
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (manager.isLoading)
        {
            // Hide the timer text if the game is loading
            timerUI.gameObject.SetActive(false);
        }
        else
        {
            // Show the timer text and update the time when the game is not loading
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
        if (scene.name == "MainMenu")
        {
            // Reset the timer and the UI text when the main menu is loaded
            elapsedTime = 0f;
            timerUI.text = "00.00.0";
        }
    }
}
