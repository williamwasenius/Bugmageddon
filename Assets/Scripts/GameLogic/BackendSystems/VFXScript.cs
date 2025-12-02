using UnityEngine;
using UnityEngine.VFX;

public class VFXScript : MonoBehaviour
{
    public int lifetime;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
