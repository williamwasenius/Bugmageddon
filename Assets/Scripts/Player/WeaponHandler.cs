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
    public bool rotatingBarrel = false;
    public int rotationSpeed;

    private void Start()
    {

    }

    public void shoot()
    {
        if (Time.time >= cooldownCounter)
        {
            if (shootSound != null)
            {
                shootSound.Play();
            }
            muzzleFlash.Play();
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            cooldownCounter = Time.time + weaponFireRate;
        }

        if (rotatingBarrel && barrel != null)
        {
            barrel.transform.Rotate(0, 0, rotationSpeed);
        }

    }

    public float GetCooldownProgress()
    {
        return Mathf.Clamp01((cooldownCounter - Time.time) / weaponFireRate);
    }

}
