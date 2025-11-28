using UnityEngine;

public class triggeremergencytest : MonoBehaviour
{
    public Collider collider;
    public string hi = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("enemy entered");
        }
    }
}
