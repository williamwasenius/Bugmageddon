using UnityEngine;
using UnityEngine.VFX;

public class WeaponHandler : MonoBehaviour
{
    private PlayerController playerController;

    [Header("Related Objects")]
    public GameObject projectile;
    public Transform firePoint;
    public Transform barrel;
    public VisualEffect muzzleFlash;

    [Header("Weapon Statistics")]
    public float weaponFireRate = 0.2f;
    private float cooldownCounter = 0;

    [Header("Weapon properties")]
    public AudioSource shootSound;
    public AudioSource chargeSound;

    public bool rotatingBarrel = false;
    public int rotationSpeed;

    public bool chargedWeapon = false;
    public bool isCharging = false;
    public bool fullyCharged = false;
    public float totalChargeTime;
    public float currentCharge;

    void Awake()
    {
        PlayerController player = GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.AssignWeapon(this);
        }
    }

    private void Start()
    {
        currentCharge = 0f;
    }
    public void StartCharging()
    {
        if (!chargedWeapon || Time.time < cooldownCounter) return;

        isCharging = true;
        currentCharge += Time.deltaTime;

        if (chargeSound != null && !chargeSound.isPlaying)
            chargeSound.Play();

        if (currentCharge >= totalChargeTime)
        {
            currentCharge = totalChargeTime;
            fullyCharged = true;
        }
    }

    public void ReleaseShot()
    {
        if (!chargedWeapon) return;

        if (fullyCharged)
        {
            ShootBullet();
        }

        currentCharge = 0f;
        fullyCharged = false;
        isCharging = false;

        cooldownCounter = Time.time + weaponFireRate;

    }

    public void AutoFire()
    {
        if (Time.time >= cooldownCounter)
        {
            ShootBullet();
        }

        if (rotatingBarrel && barrel != null)
        {
            barrel.Rotate(0, 0, rotationSpeed);
        }
    }

    private void ShootBullet()
    {
        if (Time.time < cooldownCounter) return;

        if (shootSound) shootSound.Play();
        if (muzzleFlash) muzzleFlash.Play();

        Instantiate(projectile, firePoint.position, firePoint.rotation);
        cooldownCounter = Time.time + weaponFireRate;
    }

    public float GetCooldownProgress()
    {
        if (chargedWeapon)
        {
            return Mathf.Clamp01(currentCharge / totalChargeTime);
        }
        else
        {
            return 1f - Mathf.Clamp01((cooldownCounter - Time.time) / weaponFireRate);
        }
    }

}
