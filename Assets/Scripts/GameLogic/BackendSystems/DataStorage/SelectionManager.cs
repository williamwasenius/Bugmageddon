using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    public MechStatsSO[] mechSelections;
    public GameObject[] weaponPrefabsRight;
    public GameObject[] weaponPrefabsLeft;
    public int selectedMech;
    public int selectedWeapon1;       
    public int selectedWeapon2;       

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}