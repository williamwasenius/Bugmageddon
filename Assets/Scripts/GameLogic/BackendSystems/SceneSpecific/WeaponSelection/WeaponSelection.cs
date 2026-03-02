using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelection : MonoBehaviour
{
    [System.Serializable]
    public class SelectionComponents
    {
        public GameObject informationDisplay;
        public GameObject visualDisplay;
    }

    public SelectionComponents[] mechChassis;

    public int selectedMech = 0;

    public SelectionComponents[] weaponsR;
    public SelectionComponents[] weaponsL;

    public int selectedWPNR = 0;
    public int selectedWPNL = 0;

    void Start()
    {
        Cursor.visible = true;

        Activate(weaponsR, selectedWPNR);
        Activate(weaponsL, selectedWPNL);
    }

    // ---------- MECH CHASSIS ---------- //

    public void NextMech()
    {
        SwitchComponent(mechChassis, ref selectedMech, 1);
    }
    public void PreviousMech()
    {
        SwitchComponent(mechChassis, ref selectedMech, -1);
    }

    // ---------- RIGHT WEAPON ---------- //
    public void NextWeapon()
    {
        SwitchComponent(weaponsR, ref selectedWPNR, 1);
    }

    public void PreviousWeapon()
    {
        SwitchComponent(weaponsR, ref selectedWPNR, -1);
    }

    // ---------- LEFT WEAPON ---------- //
    public void NextWeapon2()
    {
        SwitchComponent(weaponsL, ref selectedWPNL, 1);
    }

    public void PreviousWeapon2()
    {
        SwitchComponent(weaponsL, ref selectedWPNL, -1);
    }

    // ---------- WEAPON SELECTION FUNCTIONS ---------- //
    void SwitchComponent(SelectionComponents[] components, ref int index, int direction)
    {
        DeActivate(components, index);

        index = (index + direction + components.Length) % components.Length;

        Activate(components, index);
    }

    void Activate(SelectionComponents[] components, int index)
    {
        components[index].informationDisplay.SetActive(true);
        components[index].visualDisplay.SetActive(true);
    }

    void DeActivate(SelectionComponents[] components, int index)
    {
        components[index].informationDisplay.SetActive(false);
        components[index].visualDisplay.SetActive(false);
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
        SelectionManager.Instance.selectedMech = selectedMech;

        SelectionManager.Instance.selectedWeapon1 = selectedWPNR;
        SelectionManager.Instance.selectedWeapon2 = selectedWPNL;

        SceneManager.LoadScene(MissionTracker.Instance.GetNextMissionScene());
    }
}
