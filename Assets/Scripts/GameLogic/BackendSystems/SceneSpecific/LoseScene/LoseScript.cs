using UnityEngine;

public class LoseScript : MonoBehaviour
{
    void Start()
    {
        SaveManager.Instance.currentMission = 0;
        SaveManager.Instance.SavePlayerData();
    }
}
