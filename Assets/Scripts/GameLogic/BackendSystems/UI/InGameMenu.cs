using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject combatCanvas;
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
                combatCanvas.SetActive(true);
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
            else
            {
                combatCanvas.SetActive(false);
                menuCanvas.SetActive(true);
                Cursor.visible = true;
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
