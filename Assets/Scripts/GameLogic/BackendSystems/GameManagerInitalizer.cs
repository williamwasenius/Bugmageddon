using UnityEngine;

public class GameManagerInitializer : MonoBehaviour
{
    public GameObject gameManagerPrefab;

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
    }
}