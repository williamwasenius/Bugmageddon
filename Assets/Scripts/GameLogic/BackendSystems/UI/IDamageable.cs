using UnityEngine;

public interface IDamageable
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; }
    float Armor { get; }
    void TakeDamage(float damage);
}
