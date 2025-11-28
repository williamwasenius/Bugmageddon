using Unity.Cinemachine;
using UnityEngine;

public class Misison5BossFightTrigger : MonoBehaviour
{
    public GameObject wall;
    public GameObject bossUI;
    public GameObject boss;
    public CameraZoom camera;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player passed line");
            wall.SetActive(true);
            bossUI.SetActive(true);
            boss.SetActive(true);
            gameObject.SetActive(false);
            camera.StartTransition();
        }
    }
}
