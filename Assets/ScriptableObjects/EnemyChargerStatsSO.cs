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
    public bool isCharging = false;
    public float chargeCooldown;
    public float chargeRechargeTimer;
}