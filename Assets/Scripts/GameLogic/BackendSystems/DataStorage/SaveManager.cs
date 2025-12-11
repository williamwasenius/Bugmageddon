using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // Missions
    // === completed ever ===
    public bool tutorialMission;
    public bool mission1;
    public bool mission2;
    public bool mission3;
    public bool mission4;
    public bool mission5;
    // === completed this run ===
    public int currentMission;
    
    // Weapons unlocks
    public bool weapon1;
    public bool weapon2;
    public bool weapon3;
    public bool weapon4;

    // Audio settings
    public float volumeSliderAmount;
    public float musicSliderAmount;
    public float vFXSliderAmount;


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
        SavePlayerData();
    }


    public void SavePlayerData()
    {
        PlayerData data = new PlayerData();

        // Missions
        data.tutorialMission = tutorialMission;
        data.mission1 = mission1;
        data.mission2 = mission2;
        data.mission3 = mission3;
        data.mission4 = mission4;
        data.mission5 = mission5;
        data.currentMission = currentMission;

        // Weapons
        data.weapon1 = weapon1;
        data.weapon2 = weapon2;
        data.weapon3 = weapon3;
        data.weapon3 = weapon4;

        // Audio 
        data.volumeSliderAmount = volumeSliderAmount;
        data.musicSliderAmount = musicSliderAmount;
        data.vFXSliderAmount = vFXSliderAmount;

        string jason = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerInfo.json", jason);
    }
    public void LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.json"))
        {
            string jason = File.ReadAllText(Application.persistentDataPath + "/playerInfo.json");
            PlayerData data = JsonUtility.FromJson<PlayerData>(jason);

            // Missions
            tutorialMission = data.tutorialMission;
            mission1 = data.mission1;
            mission2 = data.mission2;
            mission3 = data.mission3;
            mission4 = data.mission4;
            mission5 = data.mission5;
            currentMission = data.currentMission;

            // Weapons
            weapon1 = data.weapon1;
            weapon2 = data.weapon2;
            weapon3 = data.weapon3;
            weapon4 = data.weapon4;

            // Audio
            volumeSliderAmount = data.volumeSliderAmount;
            musicSliderAmount= data.musicSliderAmount;
            vFXSliderAmount = data.vFXSliderAmount;
        }
        else
        {
            DefaultPlayerData();
            SavePlayerData();
        }
    }

    public void DefaultPlayerData()
    {
        //missions
        tutorialMission = false;
        mission1 = false;
        mission2 = false;
        mission3 = false;
        mission4 = false;
        mission5 = false;
        currentMission = 0;

        //weapons
        weapon1 = true;
        weapon2 = false;
        weapon3 = false;
        weapon4 = false;
    }
    public void DefaultGameData()
    {
        //no missions completed
    }
}

[Serializable]
class PlayerData
{
    // missions
    public bool tutorialMission;
    public bool mission1;
    public bool mission2;
    public bool mission3;
    public bool mission4;
    public bool mission5;
    public int currentMission;

    // weapons unlocks
    public bool weapon1;
    public bool weapon2;
    public bool weapon3;
    public bool weapon4;

    // audio settings
    public float volumeSliderAmount;
    public float musicSliderAmount;
    public float vFXSliderAmount;
}

//if we have different saves.
[Serializable]
class GameData
{

}
