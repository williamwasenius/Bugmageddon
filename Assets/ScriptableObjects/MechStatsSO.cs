using UnityEngine;

[CreateAssetMenu(menuName = "Player/Mechs/Core")]
public class MechStatsSO : ScriptableObject
{
    [Header("Defense")]
    public float maxHealth;
    public float armor;

    [Header("Movement")]
    public float walkingSpeed;
    public float runningSpeed;
    public float acceleartion;
    public float decelaration;
    public float legRotationSpeed;

    [Header("Weaponry")]
    public float weaponMaxRot;
    public float weaponRotSpeed;

    [Header("Model")]
    public GameObject model;
}
