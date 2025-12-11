using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public static CombatUIManager Instance { get; set; }

    public Image weaponRCooldownImage;
    public Image weaponLCooldownImage;
   // public Image DashCooldownImage;

    private WeaponHandler weaponRHandler;
    private WeaponHandler weaponLHandler;
    private PlayerController player;

    public GameObject weaponRParent; 
    public GameObject weaponLParent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        AcquireWeapons();
        UpdateCooldownUI();
    }

    private void UpdateCooldownUI()
    {
        if (weaponRHandler != null)
        {
            weaponRCooldownImage.fillAmount = weaponRHandler.GetCooldownProgress();
        }

        if (weaponLHandler != null)
        {
            weaponLCooldownImage.fillAmount = weaponLHandler.GetCooldownProgress();
        }

        if (player != null)
        {
            //DashCooldownImage.fillAmount = 1 - player.DashCooldownProgress();
        }

    }

    private WeaponHandler FindWeaponHandler(GameObject weaponParent)
    {
        if (weaponParent != null)
        {
            WeaponHandler handler = weaponParent.GetComponentInChildren<WeaponHandler>();
            return handler;
        }
        return null;
    }

    private void AcquireWeapons()
    {
        if (weaponRHandler == null)
        {
            weaponRHandler = FindWeaponHandler(weaponRParent);
        }
        if (weaponLHandler == null)
        {
            weaponLHandler = FindWeaponHandler(weaponLParent);
        }
    }

}
