using UnityEngine;

public class AttackTriggerActivator : MonoBehaviour
{
    [Header("Attack Triggers")]
    public GameObject[] attackTriggers;

    public void EnableTrigger(int index)
    {
        if (index < 0 || index >= attackTriggers.Length) return;
        attackTriggers[index].SetActive(true);
    }

    public void DisableTrigger(int index)
    {
        if (index < 0 || index >= attackTriggers.Length) return;
        attackTriggers[index].SetActive(false);
    }
}
