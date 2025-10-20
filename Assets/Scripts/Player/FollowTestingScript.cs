using UnityEngine;

public class FollowTestingScript : MonoBehaviour
{
    public Camera playerCamera;
    public Transform player;
    public float maxDistance = 1000f;
    public float followRadius = 30f;
    public float moveSpeed = 10f; // how fast the orb moves toward target
    public LayerMask groundMask;

    private Vector3 targetPosition;

    void Update()
    {
        UpdateTargetPosition();
        MoveOrb();
    }

    private void UpdateTargetPosition()
    {
        if (playerCamera == null || player == null)
            return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundMask))
        {
            Vector3 desiredPosition = hit.point;
            Vector3 offset = desiredPosition - player.position;

            if (offset.magnitude > followRadius)
                offset = offset.normalized * followRadius;

            targetPosition = player.position + offset;
        }
    }

    private void MoveOrb()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }
}
