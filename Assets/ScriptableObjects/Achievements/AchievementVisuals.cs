using UnityEngine;
using UnityEngine.Rendering;

public class AchievementVisuals : MonoBehaviour
{
    public AchievementSO[] achievementSOs;
    public GameObject achievementUIPrefab;
    public Transform parent;
    public void Start()
    {
        SetSOCompleted();

        for(int i = 0; i <achievementSOs.Length; i++)
        {
            var ui = Instantiate(achievementUIPrefab, new Vector2(975, 650 + i*-150), Quaternion.identity, parent);
            AchivementUI script = ui.GetComponent<AchivementUI>();
            script.Setup(achievementSOs[i]);
        }

        RectTransform parentRT = parent.GetComponent<RectTransform>();
        parentRT.sizeDelta = new Vector2(parentRT.sizeDelta.x, achievementSOs.Length * 150 + 250);
    }

    private void SetSOCompleted()
    {
        var achivSave = AchievementManager.Instance;
        if (achivSave.tutorialAchievement)
        {
            achievementSOs[0].unlocked = true;
        }
        if (achivSave.mission1Achievement)
        {
            achievementSOs[1].unlocked = true;
        }
        if (achivSave.mission2Achievement)
        {
            achievementSOs[2].unlocked = true;
        }
        if (achivSave.mission3Achievement)
        {
            achievementSOs[3].unlocked = true;
        }
        if (achivSave.mission4Achievement)
        {
            achievementSOs[4].unlocked = true;
        }
        if (achivSave.mission5Achievement)
        {
            achievementSOs[5].unlocked = true;
        }
    }
}
