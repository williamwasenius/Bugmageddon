using UnityEngine;

public class TriggerActions : MonoBehaviour
{
    public GameObject textOne;
    public GameObject textTwo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textOne.SetActive(false);
            textTwo.SetActive(true);
        }
    }
}
