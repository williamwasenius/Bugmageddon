using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Ranged")]
public class EnemyRangedStatsSO : ScriptableObject
{
    public GameObject projectilePrefab;
    public float projectileDamage;
    public float shootRange;
    public float followRange;
}