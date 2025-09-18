using UnityEngine;

public class Misison5BossFightTrigger : MonoBehaviour
{
    public GameObject wall;
    public GameObject bossUI;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player passed line");
            wall.SetActive(true);
            bossUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
