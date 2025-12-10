using UnityEngine;
[CreateAssetMenu(menuName = "Achievement")]
public class AchievementSO : ScriptableObject
{
    public string id; //for steam ID
    public string title;
    public string description;
    public Sprite iconLocked;
    public Sprite iconUnlocked;
    public bool unlocked; //from achievement manager
}
