using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelection : MonoBehaviour
{
    public GameObject[] weaponsR;
    public GameObject[] weaponsL;
    public int selectedWPN1 = 0;
    public int selectedWPN2 = 0;

    public void Start()
    {
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
    public void NextWeapon()
    {
        weaponsR[selectedWPN1].SetActive(false);
        selectedWPN1 = (selectedWPN1 + 1) % weaponsR.Length;
        weaponsR[selectedWPN1].SetActive(true);
    }

    public void PreviousWeapon()
    {
        weaponsR[selectedWPN1].SetActive(false);
        selectedWPN1--;
        if (selectedWPN1 < 0)
            selectedWPN1 += weaponsR.Length;
        weaponsR[selectedWPN1].SetActive(true);

    }

    public void NextWeapon2()
    {
        weaponsL[selectedWPN2].SetActive(false);
        selectedWPN2 = (selectedWPN2 + 1) % weaponsL.Length;
        weaponsL[selectedWPN2].SetActive(true);

    }

    public void PreviousWeapon2()
    {
        weaponsL[selectedWPN2].SetActive(false);
        selectedWPN2--;
        if (selectedWPN2 < 0)
            selectedWPN2 += weaponsL.Length;
        weaponsL[selectedWPN2].SetActive(true);

    }

    public void StartGame()
    {
        WeaponManager.Instance.selectedWeapon1 = selectedWPN1;
        WeaponManager.Instance.selectedWeapon2 = selectedWPN2;

        SceneManager.LoadScene(MissionTracker.Instance.GetNextMissionScene());
    }
}
