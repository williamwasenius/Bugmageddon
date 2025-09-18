using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelection : MonoBehaviour
{
    public GameObject[] weapons1;
    public GameObject[] weapons2;
    public int selectedWPN1 = 0;
    public int selectedWPN2 = 0;


    public void NextWeapon()
    {
        weapons1[selectedWPN1].SetActive(false);
        selectedWPN1 = (selectedWPN1 + 1) % weapons1.Length;
        weapons1[selectedWPN1].SetActive(true);
    }

    public void PreviousWeapon()
    {
        weapons1[selectedWPN1].SetActive(false);
        selectedWPN1--;
        if (selectedWPN1 < 0)
        {
            selectedWPN1 += weapons1.Length;
        }
        weapons1[selectedWPN1].SetActive(true);
    }

    public void NextWeapon2()
    {
        weapons2[selectedWPN2].SetActive(false);
        selectedWPN2 = (selectedWPN2 + 1) % weapons2.Length;
        weapons2[selectedWPN2].SetActive(true);
    }

    public void PreviousWeapon2()
    {
        weapons2[selectedWPN2].SetActive(false);
        selectedWPN2--;
        if (selectedWPN2 < 0)
        {
            selectedWPN2 += weapons2.Length;
        }
        weapons2[selectedWPN2].SetActive(true);
    }

    public void StartGame()
    {
            WeaponManager.Instance.selectedWeapon1 = selectedWPN1;
            WeaponManager.Instance.selectedWeapon2 = selectedWPN2;
            SceneManager.LoadScene("Mission1");
    }


}
