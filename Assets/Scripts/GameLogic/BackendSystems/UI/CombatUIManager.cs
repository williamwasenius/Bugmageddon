using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{

    public Image weapon1CooldownImage;
    public Image weapon2CooldownImage;
    public Image DashCooldownImage;

    private WeaponHandler weapon1Handler;
    private WeaponHandler weapon2Handler;
    private PlayerController player;

    public GameObject weapon1Parent; 
    public GameObject weapon2Parent;

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
        if (weapon1Handler != null)
        {
            weapon1CooldownImage.fillAmount = 1 - weapon1Handler.GetCooldownProgress();
        }

        if (weapon2Handler != null)
        {
            weapon2CooldownImage.fillAmount = 1 - weapon2Handler.GetCooldownProgress();
        }

        if (player != null)
        {
            DashCooldownImage.fillAmount = 1 - player.DashCooldownProgress();
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
        if (weapon1Handler == null)
        {
            weapon1Handler = FindWeaponHandler(weapon1Parent);
        }
        if (weapon2Handler == null)
        {
            weapon2Handler = FindWeaponHandler(weapon2Parent);
        }
    }

}
