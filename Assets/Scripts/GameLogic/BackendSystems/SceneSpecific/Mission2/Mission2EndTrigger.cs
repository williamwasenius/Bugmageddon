using UnityEngine;

public class Mission2EndTrigger : MonoBehaviour
{
    public bool EndTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndTrigger = true;
        }
    }
}
