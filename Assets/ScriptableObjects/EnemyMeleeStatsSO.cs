using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats/Melee")]
public class EnemyMeleeStatsSO : ScriptableObject
{
    public float damage;
    public float attackSpeed;
    public float attackDuration;
    public float strikeRange;
}