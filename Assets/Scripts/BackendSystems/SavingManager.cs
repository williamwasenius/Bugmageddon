using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Linq;

public class SavingManager : MonoBehaviour
{
    //save function

    //load function

    //deleate save function
    

    public static SavingManager Instance { get; private set; }
    //weapon 1 saved
    //weapon 2 saved
    //weapon unlock array?
    //mission array?

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
        //SaveGameData();
    }

    
    public void SavePlayerData()
    {
        PlayerData data = new PlayerData();

        //data.currentSave = currentSave; ===example

        string jason = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerInfo.json", jason);
    }
    public void LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.json"))
        {
            string jason = File.ReadAllText(Application.persistentDataPath + "/playerInfo.json");
            PlayerData data = JsonUtility.FromJson<PlayerData>(jason);

            
        }
        else
        {
            DefaultPlayerData();
            SavePlayerData();
        }
    }
    //Save slot specific data
    public void SaveGameData(int save)
    {
        GameData data = new GameData();



        string jason = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save" + save + ".json", jason);
    }

    public void LoadGameData(int save)
    {
        if (File.Exists(Application.persistentDataPath + "/save" + save + ".json"))
        {

        }
        else
        {
            Debug.Log("Save doesnt exist");
        }
    }

    public void DefaultPlayerData()
    {
        //weapon array?
    }
    public void DefaultGameData()
    {
        //no missions completed
    }
}

//do we need player and game separete? player for the unlocks and game for game stage?
//we had game data for different saves.
[Serializable]
class PlayerData
{
    
}

[Serializable]
class GameData
{
    
}
