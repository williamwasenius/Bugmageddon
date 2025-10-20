using UnityEngine;

public class ReactorTrigger : MonoBehaviour
{
    public Mission3Script mission;
    public int generatorIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mission.StartGeneratorDefense(generatorIndex);
        }
    }
}
