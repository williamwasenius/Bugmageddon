using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mission3Script : MonoBehaviour
{
    [Header("Mission Objects")]
    public GameObject player;
    public GameObject reactorOne;
    public GameObject reactorTwo;
    public GameObject reactorThree;
    public GameObject largeReactor;
    public GameObject enemyTargetPoint;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI objectiveText;
    public Image finalGeneratorHealthBar;

    [Header("Settings")]
    public float defendDuration = 60f;
    public float finalDefenseDuration = 120f;

    [Header("Scripts")]
    private DestructibleScript genOneStatus;
    private DestructibleScript genTwoStatus;
    private DestructibleScript genThreeStatus;
    private DestructibleScript finalGenStatus;
    public TurretScript[] turretsOne;
    public TurretScript[] turretsTwo;
    public TurretScript[] turretsThree;

    [Header("Attack Waves")]
    public AttackWaveManager waveManager;

    private float currentTimer = 0f;
    private bool timerRunning = false;

    private int completedGenerators = 0;

    private enum MissionState { Idle, DefendingGen1, DefendingGen2, DefendingGen3, FinalDefense, Complete }
    private MissionState currentState = MissionState.Idle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        genOneStatus = reactorOne.GetComponent<DestructibleScript>();
        genTwoStatus = reactorTwo.GetComponent<DestructibleScript>();
        genThreeStatus = reactorThree.GetComponent<DestructibleScript>();
        finalGenStatus = largeReactor.GetComponent<DestructibleScript>();

        objectiveText.text = "Activate and Defend the Generators!";
    }

    void Update()
    {
        if (player == null || largeReactor == null)
        {
            Lose();
            return;
        }

        UpdateFinalGeneratorHealth();

        if (timerRunning)
        {
            currentTimer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(currentTimer).ToString();

            if (currentTimer <= 0)
            {
                timerRunning = false;
                OnDefenseComplete();
            }
        }
    }

    private void UpdateFinalGeneratorHealth()
    {
        float normalizedHealth = Mathf.Clamp01(finalGenStatus.CurrentHealth / finalGenStatus.maxHealth);
        finalGeneratorHealthBar.fillAmount = normalizedHealth;
    }

    private void OnDefenseComplete()
    {
        switch (currentState)
        {
            case MissionState.DefendingGen1:
                completedGenerators++;
                waveManager.EndWave(1);
                foreach (TurretScript turret in turretsOne)
                {
                    turret.powered = true;
                }
                currentState = MissionState.Idle;
                break;
            case MissionState.DefendingGen2:
                completedGenerators++;
                waveManager.EndWave(2);
                foreach (TurretScript turret in turretsTwo)
                {
                    turret.powered = true;
                }
                currentState = MissionState.Idle;
                break;
            case MissionState.DefendingGen3:
                completedGenerators++;
                waveManager.EndWave(3);
                foreach (TurretScript turret in turretsThree)
                {
                    turret.powered = true;
                }
                currentState = MissionState.Idle;
                break;
            case MissionState.FinalDefense:
                waveManager.EndFinalWave();
                MissionComplete();
                break;
        }
    }

    public void StartGeneratorDefense(int genIndex)
    {
        if (timerRunning) return;

        switch (genIndex)
        {
            case 1:
                currentState = MissionState.DefendingGen1;
                enemyTargetPoint.transform.position = reactorOne.transform.position;
                waveManager.Wave1Attack();
                break;
            case 2:
                currentState = MissionState.DefendingGen2;
                enemyTargetPoint.transform.position = reactorTwo.transform.position;
                waveManager.Wave2Attack();
                break;
            case 3:
                currentState = MissionState.DefendingGen3;
                enemyTargetPoint.transform.position = reactorThree.transform.position;
                waveManager.Wave3Attack();
                break;
            case 4:
                currentState = MissionState.FinalDefense;
                enemyTargetPoint.transform.position = largeReactor.transform.position;
                waveManager.FinalWaveAttack();
                break;
        }

        StartCoroutine(StartDefenseTimer());
    }


    private IEnumerator StartDefenseTimer()
    {
        timerRunning = true;
        currentTimer = (currentState == MissionState.FinalDefense) ? finalDefenseDuration : defendDuration;

        objectiveText.text = $"Defending {currentState}...";
        yield return new WaitForSeconds(currentTimer);

        OnDefenseComplete();
    }

    private void MissionComplete()
    {
        currentState = MissionState.Complete;
        objectiveText.text = "Mission Complete!";
        MissionTracker.Instance.MissionComplete("Mission3");

        if (!SaveManager.Instance.mission3)
        {
            SaveManager.Instance.mission3 = true;
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
