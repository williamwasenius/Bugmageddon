using UnityEngine;

public class AchievementVisuals : MonoBehaviour
{
    public AchievementSO[] achievements;
    public GameObject achievementUIPrefab;
    public Transform parent;
    public void Start()
    {
        for(int i = 0; i <achievements.Length; i++)
        {
            var ui = Instantiate(achievementUIPrefab, new Vector2(300, 400 + i*-150), Quaternion.identity, parent);
            AchivementUI script = ui.GetComponent<AchivementUI>();
            script.Setup(achievements[i]);
        }

        RectTransform parentRT = parent.GetComponent<RectTransform>();
        parentRT.sizeDelta = new Vector2(parentRT.sizeDelta.x, achievements.Length * 150 + 250);
    }
}
