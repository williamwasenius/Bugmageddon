using UnityEngine;

public class AttackWaveManager : MonoBehaviour
{
    [Header("Waves")]
    public GameObject[][] waves;

    [Header("Wave Lists (Assign here)")]
    public GameObject[] wave1Enemies;
    public GameObject[] wave2Enemies;
    public GameObject[] wave3Enemies;
    public GameObject[] finalWaveEnemies;

    private GameObject[][] waveArray;

    void Awake()
    {
        waveArray = new GameObject[][]
        {
            wave1Enemies,
            wave2Enemies,
            wave3Enemies,
            finalWaveEnemies
        };
    }

    public void StartWave(int index)
    {
        if (index <= 0 || index > waveArray.Length)
        {
            Debug.LogWarning("Wave index out of range.");
            return;
        }

        ActivateWave(waveArray[index - 1]);
    }

    public void EndWave(int index)
    {
        if (index <= 0 || index > waveArray.Length)
        {
            Debug.LogWarning("Wave index out of range.");
            return;
        }

        DeactivateWave(waveArray[index - 1]);
    }

    private void ActivateWave(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.SetActive(true);
        }
    }

    private void DeactivateWave(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.SetActive(false);
        }
    }
}
