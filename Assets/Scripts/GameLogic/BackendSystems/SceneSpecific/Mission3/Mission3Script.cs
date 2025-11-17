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
    public GameObject currentReactor;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI objectiveText;
    public Image finalGeneratorHealthBar;

    public TextMeshProUGUI gen1Text;
    public TextMeshProUGUI gen2Text;
    public TextMeshProUGUI gen3Text;
    public TextMeshProUGUI currentReactorText;

    [Header("Settings")]
    public float defendDuration = 60f;
    public float finalDefenseDuration = 120f;

    [Header("Scripts")]
    private DestructibleScript genOneStatus;
    private DestructibleScript genTwoStatus;
    private DestructibleScript genThreeStatus;
    private DestructibleScript finalGenStatus;

    private SmallReactor smallGen1;
    private SmallReactor smallGen2;
    private SmallReactor smallGen3;

    [Header("Waves")]
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

        smallGen1 = reactorOne.GetComponent<SmallReactor>();
        smallGen2 = reactorTwo.GetComponent<SmallReactor>();
        smallGen3 = reactorThree.GetComponent<SmallReactor>();

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

            if (currentReactor == null)
            {
                OnDefenseFail();
            }
            else if (currentTimer <= 0)
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
                smallGen1.powered = true;
                currentReactorText.color = Color.green;
                currentReactor = largeReactor;
                currentState = MissionState.Idle;
                break;

            case MissionState.DefendingGen2:
                completedGenerators++;
                waveManager.EndWave(2);
                smallGen2.powered = true;
                currentReactorText.color = Color.green;
                currentReactor = largeReactor;
                currentState = MissionState.Idle;
                break;

            case MissionState.DefendingGen3:
                completedGenerators++;
                waveManager.EndWave(3);
                smallGen3.powered = true;
                currentReactorText.color = Color.green;
                currentReactor = largeReactor;
                currentState = MissionState.Idle;
                break;

            case MissionState.FinalDefense:
                waveManager.EndFinalWave();
                MissionComplete();
                break;
        }
    }

    private void OnDefenseFail()
    {
        completedGenerators++;
        currentReactorText.fontStyle = FontStyles.Strikethrough;
        currentReactorText.color = Color.red;
        waveManager.EndWave(1);
        waveManager.EndWave(2);
        waveManager.EndWave(3);
        currentState = MissionState.Idle;
    }

    public void StartGeneratorDefense(int genIndex)
    {
        if (timerRunning) return;

        switch (genIndex)
        {
            case 1:
                currentState = MissionState.DefendingGen1;
                currentReactor = reactorOne;
                currentReactorText = gen1Text;
                enemyTargetPoint.transform.position = reactorOne.transform.position;
                gen1Text.color = Color.turquoise;
                waveManager.Wave1Attack();
                break;
            case 2:
                currentState = MissionState.DefendingGen2;
                currentReactor = reactorTwo;
                currentReactorText = gen2Text;
                enemyTargetPoint.transform.position = reactorTwo.transform.position;
                waveManager.Wave2Attack();
                break;
            case 3:
                currentState = MissionState.DefendingGen3;
                currentReactor = reactorThree;
                currentReactorText = gen3Text;
                enemyTargetPoint.transform.position = reactorThree.transform.position;
                waveManager.Wave3Attack();
                break;
            case 4:
                currentState = MissionState.FinalDefense;
                currentReactor = largeReactor;
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
