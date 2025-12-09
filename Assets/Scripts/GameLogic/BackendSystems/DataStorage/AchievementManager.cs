using System;
using System.IO;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    // MissionCompletionAchievements
    public bool tutorialAchievement;
    public bool mission1Achievement;
    public bool mission2Achievement;
    public bool mission3Achievement;
    public bool mission4Achievement;
    public bool mission5Achievement;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void OnApplicationQuit()
    {
        SaveAchievementData();
    }

    public void SaveAchievementData()
    {
        AchievementData data = new AchievementData();

        // Missions
        data.tutorialAchievement = tutorialAchievement;
        data.mission1Achievement = mission1Achievement;
        data.mission2Achievement = mission2Achievement;
        data.mission3Achievement = mission3Achievement;
        data.mission4Achievement = mission4Achievement;
        data.mission5Achievement = mission5Achievement;
        

        string jason = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/AchievementData.json", jason);
    }
    public void LoadAchievementData()
    {
        if (File.Exists(Application.persistentDataPath + "/AchievementData.json"))
        {
            string jason = File.ReadAllText(Application.persistentDataPath + "/AchievementData.json");
            AchievementData data = JsonUtility.FromJson<AchievementData>(jason);

            // Missions
            tutorialAchievement = data.tutorialAchievement;
            mission1Achievement = data.mission1Achievement;
            mission2Achievement = data.mission2Achievement;
            mission3Achievement = data.mission3Achievement;
            mission4Achievement = data.mission4Achievement;
            mission5Achievement = data.mission5Achievement;
            
        }
        else
        {
            SaveAchievementData();
        }
    }

    public void GetAchievement(string achievementName)
    {
        var field = typeof(AchievementManager).GetField(
        achievementName,
        System.Reflection.BindingFlags.Instance |
        System.Reflection.BindingFlags.Public |
        System.Reflection.BindingFlags.NonPublic
        );

        if (field == null)
        {
            Debug.LogError("Achievement not found: " + achievementName);
            return;
        }
        if ((bool)field.GetValue(this))
        {
            Debug.Log("Already unlocked: " + achievementName);
            return;
        }

        field.SetValue(this, true);
        Debug.Log("Unlocked achievement: " + achievementName);

        /*
        //accornding to one tutorial
        if(steam_stats_ready())
        {
            if (!steam_set_achievement(achievementName))
            {
                steam_set_achievement(achievementName);
                steam_store_stats();
            }
        }

        //according to another
        if (SteamManager.Initialized)
        {
            bool alreadyUnlocked = false;
            SteamUserStats.GetAchievement(achievementName, out alreadyUnlocked);

            if (!alreadyUnlocked)
            {
                SteamUserStats.SetAchievement(achievementName);
                SteamUserStats.StoreStats();

                Debug.Log("Steam Achievement unlocked: " + achievementName);
            }
            else
            {
                Debug.Log("Steam Achievement already unlocked: " + achievementName);
            }
        }
        */
        SaveAchievementData();
    }


}

[Serializable]
class AchievementData
{
    // MissionCompletionAchievements
    public bool tutorialAchievement;
    public bool mission1Achievement;
    public bool mission2Achievement;
    public bool mission3Achievement;
    public bool mission4Achievement;
    public bool mission5Achievement;
}
