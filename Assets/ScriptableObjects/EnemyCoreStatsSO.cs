using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Core")]
public class EnemyCoreStatsSO : ScriptableObject
{
    public bool isPreplaced = true;

    [Header("Defense")]
    public float maxHealth;
    public float armor;

    [Header("Movement")]
    public float wanderSpeed;
    public float chaseSpeed;
    public float wanderRadius;
    public float wanderIntervals;

    [Header("Detection")]
    public float sightRange;
    public float detectionRange;

    [Header("DeathAnimation")]
    public AnimationClip deathClip;
    public float deathDuration => deathClip != null ? deathClip.length : 0f;
}
