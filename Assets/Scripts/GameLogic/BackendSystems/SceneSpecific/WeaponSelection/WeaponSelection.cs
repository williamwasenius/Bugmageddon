using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelection : MonoBehaviour
{
    [System.Serializable]
    public class WeaponSelectionComponents
    {
        public GameObject weaponDisplay;
        public GameObject weaponModel;
    }

    public WeaponSelectionComponents[] weaponsR;
    public WeaponSelectionComponents[] weaponsL;

    public int selectedWPNR = 0;
    public int selectedWPNL = 0;

    void Start()
    {
        Cursor.visible = true;

        Activate(weaponsR, selectedWPNR);
        Activate(weaponsL, selectedWPNL);
    }

    // ---------- RIGHT WEAPON ---------- //
    public void NextWeapon()
    {
        SwitchWeapon(weaponsR, ref selectedWPNR, 1);
    }

    public void PreviousWeapon()
    {
        SwitchWeapon(weaponsR, ref selectedWPNR, -1);
    }

    // ---------- LEFT WEAPON ---------- //
    public void NextWeapon2()
    {
        SwitchWeapon(weaponsL, ref selectedWPNL, 1);
    }

    public void PreviousWeapon2()
    {
        SwitchWeapon(weaponsL, ref selectedWPNL, -1);
    }

    // ---------- WEAPON SELECTION FUNCTIONS ---------- //
    void SwitchWeapon(WeaponSelectionComponents[] weapons, ref int index, int direction)
    {
        DeActivate(weapons, index);

        index = (index + direction + weapons.Length) % weapons.Length;

        Activate(weapons, index);
    }

    void Activate(WeaponSelectionComponents[] weapons, int index)
    {
        weapons[index].weaponDisplay.SetActive(true);
        weapons[index].weaponModel.SetActive(true);
    }

    void DeActivate(WeaponSelectionComponents[] weapons, int index)
    {
        weapons[index].weaponDisplay.SetActive(false);
        weapons[index].weaponModel.SetActive(false);
    }

    // ---------- OTHER FUNCTIONS ---------- //

    public void ToggleActive(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    public void SetActive(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void SetInactive(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void StartGame()
    {
        WeaponManager.Instance.selectedWeapon1 = selectedWPNR;
        WeaponManager.Instance.selectedWeapon2 = selectedWPNL;

        SceneManager.LoadScene(MissionTracker.Instance.GetNextMissionScene());
    }
}
