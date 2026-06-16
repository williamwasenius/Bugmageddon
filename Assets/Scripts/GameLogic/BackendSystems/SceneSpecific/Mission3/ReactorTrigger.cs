using UnityEngine;

public class ReactorTrigger : MonoBehaviour, IInteractable
{
    public Mission3Script mission;
    public GameObject interractiblePrompt;
    public Collider trigger;
    public GameObject indicatorCircle;
    public int generatorIndex;

    void Start()
    {
        mission = Mission3Script.instance;
        trigger = GetComponent<Collider>();
    }

    public void Activate()
    {
        interractiblePrompt.SetActive(false);
        mission.StartGeneratorDefense(generatorIndex);
        trigger.enabled = false;
        indicatorCircle.SetActive(false);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interractiblePrompt.SetActive(true);
            other.GetComponentInParent<PlayerController>().currentInteractable = this;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interractiblePrompt.SetActive(false);
            other.GetComponentInParent<PlayerController>().currentInteractable = null;
        }
    }


}
