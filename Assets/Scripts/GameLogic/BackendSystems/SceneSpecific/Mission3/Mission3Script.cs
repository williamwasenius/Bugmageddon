using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mission3Script : MonoBehaviour
{
    [System.Serializable]
    public class GeneratorData
    {
        public GameObject reactor;  
        public TextMeshProUGUI uiText;
        public DestructibleScript destructible;
        public SmallReactor smallScript;
    }

    [Header("Mission Objects")]
    public GameObject player;
    public GameObject largeReactor;
    public GameObject enemyTargetPoint;

    [Header("Generators")]
    public GeneratorData[] generators; 
    public GeneratorData finalGenerator;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI objectiveText;
    public Image finalGeneratorHealthBar;

    [Header("Settings")]
    public float defendDuration = 60f;
    public float finalDefenseDuration = 120f;

    [Header("Waves")]
    public AttackWaveManager waveManager;

    private float currentTimer = 0f;
    private bool timerRunning = false;
    private int currentGeneratorIndex = -1; 

    private enum MissionState { Idle, Defending, FinalDefense, Complete }
    private MissionState currentState = MissionState.Idle;

    private GameObject currentReactor;
    private TextMeshProUGUI currentReactorText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objectiveText.text = "Activate and Defend the Generators!";
        foreach (var gen in generators)
        {
            if (gen.reactor == null) continue;

            gen.destructible = gen.reactor.GetComponent<DestructibleScript>();
            gen.smallScript = gen.reactor.GetComponent<SmallReactor>();
        }
    }

    void Update()
    {
        if (player == null || finalGenerator.reactor == null)
        {
            Lose();
            return;
        }

        if (timerRunning)
        {
            currentTimer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(currentTimer).ToString();

            if (currentReactor == null)
            {
                timerRunning = false;
                OnDefenseFail();
                return;
            }
            else if (currentTimer <= 0)
            {
                timerRunning = false;
                OnDefenseComplete();
            }
        }
    }

    // ---------------------- DEFENSE START ----------------------

    public void StartGeneratorDefense(int index)
    {
        if (timerRunning) return;

        if (index == 4)
        {
            StartFinalDefense();
            return;
        }

        currentGeneratorIndex = index - 1;
        var gen = generators[currentGeneratorIndex];

        currentState = MissionState.Defending;
        currentReactor = gen.reactor;
        currentReactorText = gen.uiText;

        gen.uiText.color = Color.cyan;
        enemyTargetPoint.transform.position = gen.reactor.transform.position;

        waveManager.StartWave(index);
        StartDefenseTimer();
    }

    private void StartFinalDefense()
    {
        currentGeneratorIndex = -1;
        currentState = MissionState.FinalDefense;

        currentReactor = finalGenerator.reactor;
        finalGeneratorHealthBar.gameObject.SetActive(true);
       // currentReactorText = finalGenerator.uiText;

       // finalGenerator.uiText.color = Color.cyan;
        enemyTargetPoint.transform.position = finalGenerator.reactor.transform.position;

        waveManager.StartWave(4);
        StartDefenseTimer();
    }
    private void StartDefenseTimer()
    {
        timerRunning = true;
        currentTimer = (currentState == MissionState.FinalDefense) ? finalDefenseDuration : defendDuration;

        objectiveText.text = currentState == MissionState.FinalDefense
            ? "Defending Final Generator..."
            : $"Defending Generator {currentGeneratorIndex + 1}...";
    }

    // ---------------------- DEFENSE RESULT ----------------------

    private void OnDefenseComplete()
    {
        if (currentState == MissionState.FinalDefense)
        {
            waveManager.EndWave(4);
            MissionComplete();
            return;
        }

        CompleteSmallGenerator(currentGeneratorIndex);
    }


    private void CompleteSmallGenerator(int index)
    {
        var gen = generators[index];

        gen.smallScript.Powered();
        MarkGeneratorSuccess(gen.uiText);

        waveManager.EndWave(index + 1);

        currentState = MissionState.Idle;
        currentReactor = finalGenerator.reactor;
    }

    private void OnDefenseFail()
    {
        if (currentGeneratorIndex >= 0)
            MarkGeneratorFailed(generators[currentGeneratorIndex].uiText);

        waveManager.EndWave(1);
        waveManager.EndWave(2);
        waveManager.EndWave(3);

        enemyTargetPoint.transform.position = finalGenerator.reactor.transform.position;
        currentState = MissionState.Idle;
    }

    // ---------------------- UI HELPERS ----------------------

    private void MarkGeneratorFailed(TextMeshProUGUI ui)
    {
        ui.fontStyle = FontStyles.Strikethrough;
        ui.color = Color.red;
    }

    private void MarkGeneratorSuccess(TextMeshProUGUI ui)
    {
        ui.color = Color.green;
    }

    // ---------------------- MISSION END ----------------------

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
