using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchivementUI : MonoBehaviour
{
    public Image Icon;
    public TMP_Text Title;
    public TMP_Text Description;

    public void Setup(AchievementSO data)
    {
        Title.text = data.title;
        Description.text = data.description;
        Icon.sprite = data.unlocked ? data.iconUnlocked : data.iconLocked;
    }
}
