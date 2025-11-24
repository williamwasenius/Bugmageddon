using UnityEngine;

public class triggeremergencytest : MonoBehaviour
{
    public Collider collider;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entered");
        }
    }
}
