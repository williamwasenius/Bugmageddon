using UnityEngine;
using UnityEngine.VFX;

public class explosionVFX : MonoBehaviour
{
    public float duration = 1;
    public VisualEffect explosion;

    void Start()
    {
        explosion.Play();
        Destroy(gameObject, duration);
    }

}
