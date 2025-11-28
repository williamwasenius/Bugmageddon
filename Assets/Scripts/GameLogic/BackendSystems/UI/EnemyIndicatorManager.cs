using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIndicatorManager : MonoBehaviour
{
    /*[Header("Indicator Settings")]
    public RectTransform canvasTransform;
    public GameObject arrowPrefab;
    public float indicatorRadius = 200f;
    public float activationRange = 100f;
    public int minEnemiesToShow = 3;
    public float fadeStartDistance = 75f;
    public float rotationSmoothness = 10f;

    [Header("Pooling")]
    public int initialPoolSize = 20;

    private GameObject player;
    private readonly List<GameObject> arrowPool = new List<GameObject>();
    private readonly Dictionary<GameObject, GameObject> activeIndicators = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, canvasTransform);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    void Update()
    {
        if (player == null || GameManager.Instance == null)
            return;

        List<GameObject> enemies = GameManager.Instance.enemiesInScene;
        if (enemies == null || enemies.Count == 0)
        {
            ClearIndicators();
            return;
        }

        if (enemies.Count > minEnemiesToShow)
        {
            ClearIndicators();
            return;
        }

        List<GameObject> usedEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null || !enemy.activeInHierarchy)
                continue;

            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (distance > activationRange)
                continue;

            usedEnemies.Add(enemy);

            GameObject arrow = GetIndicatorForEnemy(enemy);
            if (arrow == null) continue;

            Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
            Vector3 flatDir = new Vector3(direction.x, 0, direction.z);
            float angle = Mathf.Atan2(flatDir.x, flatDir.z) * Mathf.Rad2Deg;

            Vector3 targetPos = Quaternion.Euler(0, 0, -angle) * Vector3.up * indicatorRadius;
            arrow.transform.localPosition = targetPos;

            Quaternion targetRot = Quaternion.Euler(0, 0, -angle);
            arrow.transform.localRotation = Quaternion.Slerp(arrow.transform.localRotation, targetRot, rotationSmoothness * Time.deltaTime);

            float fadeFactor = Mathf.InverseLerp(activationRange, fadeStartDistance, distance);
            float alpha = Mathf.Lerp(0f, 1f, 1 - fadeFactor);
            float scale = Mathf.Lerp(0.5f, 1f, 1 - fadeFactor);

            Image img = arrow.GetComponent<Image>();
            if (img != null)
            {
                Color color = img.color;
                color.a = alpha;
                img.color = color;
            }

            arrow.transform.localScale = Vector3.one * scale;
            arrow.SetActive(true);
        }

        List<GameObject> toDisable = new List<GameObject>();
        foreach (var kvp in activeIndicators)
        {
            if (!usedEnemies.Contains(kvp.Key))
                toDisable.Add(kvp.Key);
        }

        foreach (var enemy in toDisable)
        {
            GameObject arrow = activeIndicators[enemy];
            arrow.SetActive(false);
            activeIndicators.Remove(enemy);
            arrowPool.Add(arrow);
        }
    }

    private GameObject GetIndicatorForEnemy(GameObject enemy)
    {
        if (activeIndicators.TryGetValue(enemy, out GameObject existing))
            return existing;

        if (arrowPool.Count == 0)
        {
            GameObject newArrow = Instantiate(arrowPrefab, canvasTransform);
            newArrow.SetActive(false);
            arrowPool.Add(newArrow);
        }

        GameObject arrow = arrowPool[0];
        arrowPool.RemoveAt(0);
        arrow.SetActive(true);
        activeIndicators[enemy] = arrow;
        return arrow;
    }

    private void ClearIndicators()
    {
        foreach (var kvp in activeIndicators)
        {
            kvp.Value.SetActive(false);
            arrowPool.Add(kvp.Value);
        }
        activeIndicators.Clear();
    }*/
}
