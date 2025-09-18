using UnityEngine;

public class Billboard : MonoBehaviour
{

    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

}
