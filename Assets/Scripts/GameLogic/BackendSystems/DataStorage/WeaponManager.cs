using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public GameObject[] weaponPrefabsRight;
    public GameObject[] weaponPrefabsLeft;
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