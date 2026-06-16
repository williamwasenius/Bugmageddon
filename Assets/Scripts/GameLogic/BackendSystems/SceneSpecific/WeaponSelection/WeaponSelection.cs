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
    [System.Serializable]
    public class MechWeaponSet
    {
        public SelectionComponents[] weaponsR;
        public SelectionComponents[] weaponsL;
    }
    public SelectionComponents[] mechChassis;
    
    public MechWeaponSet[] mechWeaponSets;
    public int selectedMech = 0;
    public int selectedWPNR = 0;
    public int selectedWPNL = 0;
    public bool selectedWPNRAlt = false;
    public bool selectedWPNLAlt = false;

    private SelectionComponents[] weaponsR
    {
        get 
        { 
            return mechWeaponSets[selectedMech].weaponsR; 
        }
    }
    private SelectionComponents[] weaponsL
    {
        get 
        { 
            return mechWeaponSets[selectedMech].weaponsL; 
        }
    }


    void Start()
    {
        Cursor.visible = true;
        Activate(weaponsR, selectedWPNR);
        Activate(weaponsL, selectedWPNL);
    }
    // ---------- MECH CHASSIS ---------- //
    public void NextMech()
    {
        SwitchMech(1);
    }

    public void PreviousMech()
    {
        SwitchMech(-1);
    }

    void SwitchMech(int direction)
    {
        DeActivate(weaponsR, selectedWPNR);
        DeActivate(weaponsL, selectedWPNL);
        SwitchComponent(mechChassis, ref selectedMech, direction);
        selectedWPNR = 0;
        selectedWPNL = 0;
        Activate(weaponsR, selectedWPNR);
        Activate(weaponsL, selectedWPNL);
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

    public void SwapWeaponVersionR()
    {
        selectedWPNRAlt = !selectedWPNRAlt;
    }
    public void SwapWeaponVersionL()
    {
        selectedWPNLAlt = !selectedWPNLAlt;
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
        SelectionManager.Instance.selectedWeaponR = selectedWPNR;
        SelectionManager.Instance.selectedWeaponL = selectedWPNL;
        SelectionManager.Instance.altWeaponR = selectedWPNRAlt;
        SelectionManager.Instance.altWeaponL = selectedWPNLAlt;

        SceneManager.LoadScene(MissionTracker.Instance.GetNextMissionScene());
    }
}