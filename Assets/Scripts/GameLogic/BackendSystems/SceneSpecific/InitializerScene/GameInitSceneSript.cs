using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitSceneSript : MonoBehaviour
{
    public GameObject gameManager;

    void Update()
    {
        if (gameManager != null)
        {
            StartCoroutine(startUp());
        }
        else
        {
            Application.Quit();
        }
    }

    private IEnumerator startUp()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainMenu");
    }
}
