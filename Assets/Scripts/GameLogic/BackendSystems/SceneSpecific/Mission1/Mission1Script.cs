using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Mission1Script : MonoBehaviour
{
    public GameObject player;
    public int enemiesRemaining;
    public TextMeshProUGUI enemyCounter;

    private void Awake()
    {
        UpdateEnemyCount();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        enemiesRemaining = EntityManagerScript.Instance.enemiesInScene.Count;
        enemiesRemaining += GameObject.FindGameObjectsWithTag("Spawner").Length;
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
