using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Detonator")]
public class EnemyDetonatorStatsSO : ScriptableObject
{
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionPrefab;
}