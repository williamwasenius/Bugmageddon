using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Ranged")]
public class EnemyRangedStatsSO : ScriptableObject
{
    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileDamage;

    [Header("Ranges")]
    public float shootRange;
    public float followRange;

    [Header("Animation")]
    public AnimationClip shootClip;
    public float ShootDuration => shootClip != null ? shootClip.length : 0.5f;
}