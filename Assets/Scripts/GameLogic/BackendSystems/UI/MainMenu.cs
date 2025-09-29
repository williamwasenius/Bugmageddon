using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;

    public void Start()
    {
        if (Cursor.visible == false)
        {  
            Cursor.visible = true; 
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("WeaponSelection");
    }

    public void Options()
    {
        options.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
