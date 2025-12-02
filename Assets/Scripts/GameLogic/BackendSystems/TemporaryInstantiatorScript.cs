using UnityEngine;

public class TemporaryInstantiatorScript : MonoBehaviour
{
    public GameObject[] objectToInstantiate;
    public Transform instantiationPosition;

    public void InstantiateObjects()
    {
        foreach (GameObject obj in objectToInstantiate)
        {
            Instantiate(obj, instantiationPosition.position, transform.rotation);
        }
    }

}
