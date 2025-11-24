using UnityEngine;
using UnityEngine.VFX;

public class DestroyVFX : MonoBehaviour
{
    private VisualEffect visualEffect;
    private bool hasPlayed;

    void Awake()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    void Update()
    {
        // Check if there are no alive particles and the effect has previously played.
        if (visualEffect.aliveParticleCount == 0 && hasPlayed)
        {
            // Return the GameObject to the object pool.
            ReturnToPool();

            // Reset the hasPlayed flag to prepare for the next time the effect is played.
            hasPlayed = false;
            return;
        }

        // If there are alive particles, mark the effect as having been played.
        if (visualEffect.aliveParticleCount > 0)
        {
            hasPlayed = true;
        }
    }

    /// <summary>
    /// Deactivates the GameObject and triggers the return to object pool process.
    /// </summary>
    public void ReturnToPool()
    {
        // Call to the ObjectPoolManager to handle the actual return-to-pool logic.
        Destroy(gameObject);
    }
}