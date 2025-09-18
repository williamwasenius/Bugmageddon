using UnityEngine;
using UnityEngine.SceneManagement;

public class VLContinue : MonoBehaviour
{
    public void Continue()
        {
        SceneManager.LoadScene("MainMenu");
        }
}
