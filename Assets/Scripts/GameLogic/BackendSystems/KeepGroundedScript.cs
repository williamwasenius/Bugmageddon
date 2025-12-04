using UnityEngine;

public class KeepGroundedScript : MonoBehaviour
{

    public float rayLength;
    public float groundHeight;
    public bool isGrounded;
    public bool aboveGround;

    public LayerMask terrainMask; 

    public void FixedUpdate()
    {
        if (isGrounded)
        {
            Ray();
        }
        if (aboveGround)
        {
            //transform.position.y = groundHeight;
        }
    }

    public void Ray()
    {

        Ray ray = new Ray(transform.position + Vector3.up, -transform.up);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {
            if (hit.collider.gameObject.layer == terrainMask)
            {
                groundHeight = hit.collider.gameObject.transform.position.y;
                aboveGround = true;
            }
            else
            {
                aboveGround = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == terrainMask)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == terrainMask)
        {
            isGrounded = false;
        }
    }

}
