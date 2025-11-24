using UnityEngine;
using UnityEngine.VFX;

public class footprintstest : MonoBehaviour
{
    public GameObject prefab; 

    public void OnTriggerEnter(Collider ground)
    {
        if(ground.tag == "ground")
        {
            Instantiate(prefab);
        }
    }

}

