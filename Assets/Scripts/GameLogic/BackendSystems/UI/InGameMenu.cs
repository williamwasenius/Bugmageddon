using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject options;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                menu.SetActive(true);
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
