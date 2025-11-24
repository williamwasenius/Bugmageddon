using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Charger")]
public class EnemyChargerStatsSO : ScriptableObject
{
    public float damageMultiplier;
    public float chargeSpeed;
    public float minChargeRange;
    public float chargeRange;
    public float chargeDuration;
    public float chargeCooldown;
}