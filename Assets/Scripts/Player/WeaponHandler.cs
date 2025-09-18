using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject projectile;
    public Transform firePoint;

    public float weaponFireRate = 0.2f;
    private float cooldownCounter = 0;

    private void Start()
    {
    }

    public void shoot()
    {
        if (Time.time >= cooldownCounter)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            cooldownCounter = Time.time + weaponFireRate;
        }
    }

    public float GetCooldownProgress()
    {
        return Mathf.Clamp01((cooldownCounter - Time.time) / weaponFireRate);
    }

}
