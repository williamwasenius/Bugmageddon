using UnityEngine;
using UnityEngine.VFX;

public class footprintstest : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawnlocation;

    public void OnTriggerEnter(Collider ground)
    {
        Instantiate(prefab, spawnlocation.transform.position, Quaternion.identity);
    }

}

