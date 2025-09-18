using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mission3Script : MonoBehaviour
{
    public GameObject objective;
    public GameObject player;

    public TextMeshProUGUI timerText;
    public Image objectiveHealthbar;
    private DestructibleScript objectiveStatus;


    public float missionDuration = 180f;
    public float timeRemaining = 0;

    public bool mission3Complete = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objectiveStatus = objective.GetComponent<DestructibleScript>();

        timeRemaining = missionDuration;

    }

    public void Update()
    {
        float normalizedHealth = Mathf.Clamp01(objectiveStatus.CurrentHealth / objectiveStatus.maxHealth);
        objectiveHealthbar.fillAmount = normalizedHealth;

        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0f;
            MissionComplete();
        }

        timerText.text = Mathf.Ceil(timeRemaining).ToString();

    }

    public void FixedUpdate()
    {
        if (player == null || objective == null)
        {
            Lose();
        }
    }

    private void MissionComplete()
    {
        Debug.Log("Congratulations! You completed the mission.");
        mission3Complete = true;
        SceneManager.LoadScene("Mission4");
        // missionLoader.LoadLevelBtn("Mission4");
    }
    private void Lose()
    {
        Debug.Log("You lost");
        SceneManager.LoadScene("GameLoss");
        // missionLoader.LoadLevelBtn("MainMenu");
    }

}
