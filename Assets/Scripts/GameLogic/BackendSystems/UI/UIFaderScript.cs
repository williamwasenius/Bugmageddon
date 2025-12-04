using UnityEngine;
using System.Collections;

public static class UIFaderScript
{
    public static void FadeIn(CanvasGroup cg, float duration, MonoBehaviour runner)
    {
        runner.StartCoroutine(FadeRoutine(cg, 1f, duration));
    }

    public static void FadeOut(CanvasGroup cg, float duration, MonoBehaviour runner)
    {
        runner.StartCoroutine(FadeRoutine(cg, 0f, duration));
    }
    private static IEnumerator FadeRoutine(CanvasGroup cg, float target, float duration)
    {
        float start = cg.alpha;
        float t = 0;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }

        cg.alpha = target;
        cg.interactable = target == 1;
        cg.blocksRaycasts = target == 1;
    }
}