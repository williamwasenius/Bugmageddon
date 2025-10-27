using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitSceneSript : MonoBehaviour
{
    public GameObject gameManager;

    void Update()
    {
        if (gameManager != null)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Application.Quit();
        }
    }
}
