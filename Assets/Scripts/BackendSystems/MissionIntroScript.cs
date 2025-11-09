using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Playables;

public class MissionIntroScript : MonoBehaviour
{
    [Header("UI References")]
    public GameObject combatUI;
    public GameObject missionUI;
    public Image blackScreen;
    public float fadeDuration = 1f;

    [Header("Cutscene")]
    public PlayableDirector cutsceneDirector;

    private GameManager gameManager;

    private void Start()
    {
        if (Time.timeScale == 1f)
            Time.timeScale = 0f;

        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance is null!");
        }
    }

    public void Continue()
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;

        if (cutsceneDirector != null)
        {

            cutsceneDirector.gameObject.SetActive(true);
            StartCoroutine(FadeOutThenPlayCutscene());
        }
        else
        {
            StartCoroutine(FadeOutThenStartGameplay());
        }
    }

    private IEnumerator FadeOutThenPlayCutscene()
    {
        float elapsedTime = 0f;
        Color color = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }

        color.a = 0f;
        blackScreen.color = color;

        gameObject.SetActive(false);

        cutsceneDirector.stopped += OnCutsceneFinished;
        cutsceneDirector.Play();
    }

    private void OnCutsceneFinished(PlayableDirector director)
    {
        cutsceneDirector.stopped -= OnCutsceneFinished;
        cutsceneDirector.gameObject.SetActive(false);

        StartCoroutine(FadeOutThenStartGameplay());
    }

    private IEnumerator FadeOutThenStartGameplay()
    {
        yield return null;

        gameObject.SetActive(false);
        combatUI.SetActive(true);
        missionUI.SetActive(true);

        gameManager.isLoading = false;
    }
}
