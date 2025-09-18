using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public GameObject menu;

    public void Confirm()
    {
        if (menu != null)
        {
            menu.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
