using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public float sightRange;
    public LayerMask mask;

    public Transform DetectPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, sightRange, mask))
            if (hit.collider.CompareTag("Player")) return hit.transform;
        return null;
    }
}