using Unity.VisualScripting;
using UnityEngine;

public static class MissionScriptUniversalFunctions
{
    public static void CompleteMission(string missionName, CanvasGroup winCanvas, MonoBehaviour runner, bool finishMission)
    {
        MissionTracker.Instance.MissionComplete(missionName);

        var save = SaveManager.Instance;

        switch (missionName)
        {
            case "Mission1": save.mission1 = true; break;
            case "Mission2": save.mission2 = true; break;
            case "Mission3": save.mission3 = true; break;
            case "Mission4": save.mission4 = true; break;
            case "Mission5": save.mission5 = true; break;
        }

        save.SavePlayerData();
        if (CombatUIManager.Instance != null)
        {
            CombatUIManager.Instance.gameObject.SetActive(false);
        }

        if (finishMission)
        {
            if (winCanvas && runner)
            {
                winCanvas.gameObject.SetActive(true);
                UIFaderScript.FadeIn(winCanvas, 2f, runner);
            }
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public static void FailMission(CanvasGroup failCanvas, MonoBehaviour runner,bool finishMission)
    {
        MissionTracker.Instance.MissionFailed();
        if (CombatUIManager.Instance != null)
        {
            CombatUIManager.Instance.gameObject.SetActive(false);
        }
        if (finishMission)
        {
            if (failCanvas && runner)
            {
                failCanvas.gameObject.SetActive(true);
                UIFaderScript.FadeIn(failCanvas, 2f, runner);
            }
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}
