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
    public AudioClip shootSound;
    public AudioClip chargeSound;
    public VisualEffect muzzleFlash;
}

