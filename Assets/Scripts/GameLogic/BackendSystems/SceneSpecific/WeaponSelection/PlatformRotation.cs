using UnityEngine;

public class PlatformRotation : MonoBehaviour
{

    public GameObject platform;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
                platform.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);

        }
    }
}
