using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission1Script : MonoBehaviour
{
    public GameObject player;
    public int enemiesRemaining;
    public TextMeshProUGUI enemyCounter;

    private void Awake()
    {
        UpdateEnemyCount();
    }

    public void Update()
    {
        enemyCounter.text = enemiesRemaining.ToString();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            Lose();
        }

        UpdateEnemyCount();

        if (enemiesRemaining <= 0)
        {
            Debug.Log("Mission Complete! All enemies are defeated.");
            MissionComplete();
        }
    }

    private void UpdateEnemyCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        enemiesRemaining = enemies.Length + spawners.Length;
    }

    private void MissionComplete()
    {

        MissionTracker.Instance.MissionComplete("Mission1");

        if (!SaveManager.Instance.mission1)
        {
            SaveManager.Instance.mission1 = true;
            SaveManager.Instance.SavePlayerData();
        }

        SceneManager.LoadScene("WeaponSelection");
    }

    private void Lose()
    {
        MissionTracker.Instance.MissionFailed();
        SceneManager.LoadScene("GameLoss");
    }

}
