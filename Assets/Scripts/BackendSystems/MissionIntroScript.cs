using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class MissionIntroScript : MonoBehaviour
{
    public GameObject combatUI;
    public GameObject missionUI;
    private GameManager gameManager;
    public Image blackScreen; 
    public float fadeDuration = 1f;

    public void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance is null!");
        }
    }

    public void Continue()
    {
        StartCoroutine(FadeOutCanvas());
    }

    private IEnumerator FadeOutCanvas()
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

        color.a = 0; 
        blackScreen.color = color;

        gameObject.SetActive(false);
        combatUI.SetActive(true);
        missionUI.SetActive(true);

        gameManager.isLoading = false;
    }
}