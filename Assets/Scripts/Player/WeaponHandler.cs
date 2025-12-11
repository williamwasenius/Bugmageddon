using UnityEngine;
using UnityEngine.VFX;

public class WeaponHandler : MonoBehaviour
{
    [Header("Weapon Information")]
    public WeaponStatsSO weaponStats;
    public Transform firePoint;
    public Transform barrel;
    private VisualEffect firePointVFX;

    private bool isFiring;

    private float cooldownTimer = 0f;
    private AudioSource source;

    [Header("Charge Weapon Specific")]
    public VisualEffect chargeProgressVFX;
    private float chargeProgress = 0f;
    private bool isCharging = false;
    private bool isCharged = false;

    [Header("Shooter")]
    public GameObject shooter;

    private void Awake()
    {
        PlayerController player = GetComponentInParent<PlayerController>(); 
        if (player != null) 
        {
            Debug.Log("player found");
            player.AssignWeapon(this); 
        }
    }

    private void Start()
    {
        shooter = transform.root.gameObject; 
        source = GetComponent<AudioSource>();
        firePointVFX = firePoint.GetComponent<VisualEffect>();
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (isFiring && weaponStats.rotatingBarrel)
        {
            rotateWeapons();
        }
    }

    public void TryFire()
    {
        if (weaponStats.chargedWeapon)
        {
            ChargeWeapon();
            return;
        }

        if (cooldownTimer <= 0)
        {
            Fire();
        }
    }
    public void StopFire()
    {
        isFiring = false;
    }

    private void ChargeWeapon()
    {
        if (!isCharging)
        {
            isCharging = true;
            chargeProgress = 0f;

            if (weaponStats.chargeSound && source != null)
                source.PlayOneShot(weaponStats.chargeSound);
            if (chargeProgressVFX != null)
                chargeProgressVFX.Play();

        }

        chargeProgress += Time.deltaTime;

        if (chargeProgress >= weaponStats.chargeTime)
        {
            isCharged = true;
        }
    }

    public void ReleaseChargeShot()
    {
        if (!weaponStats.chargedWeapon)
            return;

        if (isCharged)
        {
            Fire();
            ChargeShootConclusion();
        }
        else
        {
            ChargeShootConclusion();

        }
    }

    public void ChargeShootConclusion()
    {

        chargeProgressVFX.Stop();
        source.Stop();

        isCharging = false;
        isCharged = false;
        chargeProgress = 0f;

        cooldownTimer = weaponStats.fireRate;
    }

    public void rotateWeapons()
    {
        barrel.Rotate(Vector3.forward * weaponStats.rotationSpeed * Time.deltaTime);
    }

    private void Fire()
    {
        isFiring = true;

        ProjectileStatsSO projectileStats = weaponStats.projectileStats;
        GameObject projectile;

        if (ProjectilePoolerScript.Instance != null)
        {
            projectile = ProjectilePoolerScript.Instance.Spawn(projectileStats.projectilePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            projectile = Instantiate(projectileStats.projectilePrefab, firePoint.position, firePoint.rotation);
        }

        ProjectileLogic projectileLogic = projectile.GetComponent<ProjectileLogic>();
        projectileLogic.projectileStats = projectileStats;
        projectileLogic.shooter = shooter;

        if (firePointVFX != null)
            firePointVFX.Play();

        if (weaponStats.shootSound != null)
            source.PlayOneShot(weaponStats.shootSound);

        cooldownTimer = weaponStats.fireRate;

    }

    public float GetCooldownProgress()
    {
        if (weaponStats.chargedWeapon)
        {
            return Mathf.Clamp01(chargeProgress / weaponStats.chargeTime);
        }
        else
        {
            return 1f - Mathf.Clamp01(cooldownTimer / weaponStats.fireRate);
        }
    }


}
