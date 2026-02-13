using UnityEngine;

public static class UniversalFunctionsScript
{
    public static void ToggleObject(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
