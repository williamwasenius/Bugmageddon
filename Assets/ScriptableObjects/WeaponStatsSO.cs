using AudioSystem;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(menuName = "Stats/Weapon Stats")]
public class WeaponStatsSO : ScriptableObject
{
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

    [Header("VFX & SFX")]
    public AudioData shootSound;
    public string audioDataID = nameof(shootSound);
    public AudioData chargeSound;
    public VisualEffect muzzleFlash;
}

