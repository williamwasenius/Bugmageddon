using AudioSystem;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(menuName = "Stats/Weapon Stats")]
public class WeaponStatsSO : ScriptableObject
{
    public string weaponName = "Default";

    [Header("Firing")]
    public float fireRate = 0.2f;

    [Header("Projectile")]
    public ProjectileStatsSO projectileStats;

    [Header("WeaponFeatures")]
    public bool rotatingBarrel;
    public float rotationSpeed;

    [Header("Charging (optional)")]
    public bool chargedWeapon = false;
    public float chargeTime = 1f;

    [Header("Heat Buildup (optional)")]
    public bool buildsHeat = false;
    public float heatPerShot = 1f;
    public float maxHeat = 10f;
    public float maxCooldownDuration = 5f;

    [Header("Firerate Ramp (optional)")]
    public bool rampingFirerate = false;
    public float rampSpeed = 1f;
    public float startFireRate = 1f;

    [Header("VFX & SFX")]
    public AudioData shootSound;
    public string audioDataID = nameof(shootSound);
    public AudioData chargeSound;
    public VisualEffect muzzleFlash;

}

