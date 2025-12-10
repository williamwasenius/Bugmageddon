using UnityEngine;

public class TriggerActions : MonoBehaviour
{
    public bool textTrigger;
    public GameObject textOne;
    public GameObject textTwo;

    public Collider trigger;

    public bool musicTrigger;
    public MusicManagerScript musicManager;

    private void Start()
    {
        trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (textTrigger)
            {
                textOne.SetActive(false);
                textTwo.SetActive(true);
            }
            if (musicTrigger)
            {
                StartCoroutine(musicManager.PlayMusic());
            }

            trigger.enabled = false;
        }
    }
}
