using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Charger")]
public class EnemyChargerStatsSO : ScriptableObject
{
    [Header("Charge Damage")]
    public float chargeDamage;

    [Header("Charge Distances")]
    public float chargeSpeed;
    public float minChargeRange;
    public float chargeRange;
    public float chargeDuration;

    [Header("Charge Cooldown")]
    public float chargeCooldown;

    [Header("ChargeAnimation")]
    public AnimationClip windUp;
    public float windUpDuration => windUp != null ? windUp.length : 0f;
}