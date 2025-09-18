using UnityEngine;

public class explosionVFX : MonoBehaviour
{
    public float duration = 1;
    void Start()
    {
        Destroy(gameObject, duration);
    }

}
