using UnityEngine;
using UnityEngine.SceneManagement;

public class VLContinue : MonoBehaviour
{
    public void Start()
    {
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }

    public void Continue()
        {
        SceneManager.LoadScene("MainMenu");
        }
}
