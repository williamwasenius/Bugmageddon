using UnityEngine;

public class AttackWaveManager : MonoBehaviour
{
    [Header("Waves")]
    public GameObject[] wave1Enemies;
    public GameObject[] wave2Enemies;
    public GameObject[] wave3Enemies;
    public GameObject[] finalWaveEnemies;

    public void Wave1Attack() => ActivateWave(wave1Enemies);
    public void Wave2Attack() => ActivateWave(wave2Enemies);
    public void Wave3Attack() => ActivateWave(wave3Enemies);
    public void FinalWaveAttack() => ActivateWave(finalWaveEnemies);

    public void EndWave(int waveIndex)
    {
        switch (waveIndex)
        {
            case 1: DeactivateWave(wave1Enemies); break;
            case 2: DeactivateWave(wave2Enemies); break;
            case 3: DeactivateWave(wave3Enemies); break;
        }
    }

    public void EndFinalWave()
    {
        DeactivateWave(finalWaveEnemies);
    }

    private void ActivateWave(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) enemy.SetActive(true);
        }
    }

    private void DeactivateWave(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) enemy.SetActive(false);
        }
    }
}
