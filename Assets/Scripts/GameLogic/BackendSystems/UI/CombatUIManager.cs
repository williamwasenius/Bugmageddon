using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public static CombatUIManager Instance { get; set; }

    public Image weaponRCooldownImage;
    public Image weaponLCooldownImage;
    public Image weaponRHeatImage;
    public Image weaponLHeatImage;
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

            if (weaponRHandler.weaponStats.buildsHeat)
            {
                weaponRHeatImage.fillAmount = Mathf.Clamp01(weaponRHandler.currentHeat / weaponRHandler.weaponStats.maxHeat);
            }

        }

        if (weaponLHandler != null)
        {
            weaponLCooldownImage.fillAmount = weaponLHandler.GetCooldownProgress();
            
            if (weaponLHandler.weaponStats.buildsHeat)
            {
                weaponLHeatImage.fillAmount = Mathf.Clamp01(weaponLHandler.currentHeat / weaponLHandler.weaponStats.maxHeat);
            }
        }

        if (player != null)
        {
            //DashCooldownImage.fillAmount = 1 - player.DashCooldownProgress();
        }

    }

    /*private WeaponHandler FindWeaponHandler(GameObject weaponParent)
    {
        if (weaponParent != null)
        {
            WeaponHandler handler = weaponParent.GetComponentInChildren<WeaponHandler>();
            return handler;
        }
        return null;
    }*/

    private void AcquireWeapons()
    {
        if (weaponRHandler == null)
        {
            weaponRHandler = player.weaponRHandler;

            if (weaponRHandler.weaponStats.buildsHeat)
            {
                weaponRHeatImage.gameObject.SetActive(true);
            }
            else
            {
                weaponRHeatImage.gameObject.SetActive(false);
            }
        }
        if (weaponLHandler == null)
        {
            weaponLHandler = player.weaponLHandler;

            if (weaponLHandler.weaponStats.buildsHeat)
            {
                weaponLHeatImage.gameObject.SetActive(true);
            }
            else
            {
                weaponLHeatImage.gameObject.SetActive(false);
            }
        }
    }

}
