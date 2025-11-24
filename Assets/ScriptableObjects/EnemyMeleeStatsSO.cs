using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Melee")]
public class EnemyMeleeStatsSO : ScriptableObject
{
    [Header("Damage")]
    public float damage;

    [Header("Attack Timing")]
    public float attackSpeed;

    [Header("Range")]
    public float strikeRange;

    [Header("Animation")]
    public AnimationClip attackClip;
    public float attackDuration => attackClip != null ? attackClip.length : 0.5f;
}