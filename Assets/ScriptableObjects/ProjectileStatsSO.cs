using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectiles/Projectile")]
public class ProjectileStatsSO : ScriptableObject
{
    [Header("Projectile Core")]
    public float speed;
    public float damage;
    public float armorPierce;
    public float lifeTime;

    [Header("Piercing & Explosive")]
    public bool pierce = false;

    public bool explosive = false;
    public float explosionRadius;
    public float explosionDamage;

    [Header("Prefab References")]
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;

    [Header("Projectile Audio")]
    public AudioData flightSound;
    public AudioData hitSound;
}
