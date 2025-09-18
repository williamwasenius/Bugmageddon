using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mission1Script : MonoBehaviour
{
    public GameObject player;
    public int enemiesRemaining;
    public TextMeshProUGUI enemyCounter;
    public bool mission1complete = false;

    private void Awake()
    {
        UpdateEnemyCount();
    }
    public void Update()
    {
        enemyCounter.text = (enemiesRemaining.ToString());
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
        Debug.Log("Congratulations! You completed the mission.");
        mission1complete = true;
        SceneManager.LoadScene("Mission2");
    }
    private void Lose()
    {
        SceneManager.LoadScene("GameLoss");
    }
}