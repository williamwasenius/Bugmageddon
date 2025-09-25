using UnityEngine;

public class GameManagerInitializer : MonoBehaviour
{
    public GameObject gameManagerPrefab;
    public GameObject saveManagerPrefab;

    void Start()
    {
        GameManager existingGameManager = Object.FindAnyObjectByType<GameManager>();
        if (existingGameManager == null)
        {
            Instantiate(gameManagerPrefab);
            Debug.Log("GameManager instantiated from prefab.");
        }
        else
        {
            Debug.Log("GameManager already exists in the scene.");
        }

        SaveManager existingSaveManager = Object.FindAnyObjectByType<SaveManager>();
        if (existingGameManager == null)
        {
            Instantiate(saveManagerPrefab);
            Debug.Log("GameManager instantiated from prefab.");
        }
        else
        {
            Debug.Log("GameManager already exists in the scene.");
        }
        SaveManager.Instance.LoadPlayerData(); //Load
    }
}