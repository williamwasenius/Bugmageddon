using UnityEngine;
using System.Collections;

public class RandomToggle : MonoBehaviour
{
    public GameObject targetObject;
    public float minInactiveDelay = 1f;
    public float maxInactiveDelay = 2f;
    public float minActiveDelay = 1f;
    public float maxActiveDelay = 2f;

    private void Start()
    {
        StartCoroutine(ToggleRoutine());
    }

    private IEnumerator ToggleRoutine()
    {
        while (true)
        {
            if (targetObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(Random.Range(minActiveDelay, maxActiveDelay));
                targetObject.SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(minInactiveDelay, maxInactiveDelay));
                targetObject.SetActive(true);
            }
        }
    }
}
