using UnityEngine;

public class ReactorTrigger : MonoBehaviour, IInteractable
{
    public Mission3Script mission;
    public GameObject interractiblePrompt;
    public Collider trigger;
    public int generatorIndex;

    void Start()
    {
        trigger = GetComponent<Collider>();
    }

    public void Activate()
    {
        interractiblePrompt.SetActive(false);
        trigger.enabled = false;
        mission.StartGeneratorDefense(generatorIndex);
    }

    public void OnTriggerEnter(Collider other)
    {
        interractiblePrompt.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        interractiblePrompt.SetActive(false);
    }

}
