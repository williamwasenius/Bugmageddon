using UnityEngine;

public class WeaponSelectionRotateMech : MonoBehaviour
{
    public Transform mech;
    public float rotationSpeed = 90f;

    void Update()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            input = 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input = -1f;
        }

        mech.Rotate(Vector3.up, input * rotationSpeed * Time.deltaTime, Space.World);
    }
}
