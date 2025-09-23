using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject menu;
    public GameObject options;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuCanvas.activeSelf)
            {
                menuCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                menuCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
        }
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
