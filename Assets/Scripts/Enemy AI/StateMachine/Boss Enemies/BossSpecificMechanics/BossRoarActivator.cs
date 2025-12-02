using UnityEngine;

public class BossAbilityActivator : MonoBehaviour
{
    public GameObject broodMotherTail;

    public void TailStrikeEnable()
    {
        broodMotherTail.SetActive(true);
    }
    public void TailStrikeDisable()
    {
        broodMotherTail.SetActive(false);
    }
    public void SummonBrood()
    {
        BossEvents.OnQueenRoar?.Invoke();
    }
}
