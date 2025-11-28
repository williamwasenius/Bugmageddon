using UnityEngine;

public class MouseFollowTarget : MonoBehaviour
{
    public enum FollowMode
    {
        Direct,
        Orbit
    }

    [Header("Mode")]
    public FollowMode followMode = FollowMode.Orbit;

    [Header("References")]
    public Camera playerCamera;
    public Transform player;

    [Header("Radius Control")]
    public float maxDistance = 1000f;
    public float followRadius = 30f;
    public float minRadius = 0f;
    public float radiusSmoothness = 0.9f;

    [Header("Movement")]
    public float moveSpeed = 10f;

    [Header("Ground Detection")]
    public LayerMask groundMask;

    private Vector3 targetPosition;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("No Player assigned!");
            enabled = false;
            return;
        }

        transform.position = player.position;
        targetPosition = player.position;
    }

    private void Update()
    {
        if (playerCamera == null)
            return;

        UpdateTargetPosition();

        if (followMode == FollowMode.Direct)
            MoveDirect();
        else
            MoveOrbit();
    }

    private void UpdateTargetPosition()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundMask))
        {
            Vector3 desiredPosition = hit.point;
            desiredPosition.y = player.position.y;

            Vector3 offset = desiredPosition - player.position;
            float dist = offset.magnitude;

            dist = Mathf.Clamp(dist, minRadius, followRadius);

            if (followMode == FollowMode.Orbit)
                dist = Mathf.Lerp(offset.magnitude, dist, radiusSmoothness);

            offset = offset.normalized * dist;
            targetPosition = player.position + offset;
        }
    }

    private void MoveDirect()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    private void MoveOrbit()
    {
        Vector3 currentOffset = transform.position - player.position;
        Vector3 targetOffset = targetPosition - player.position;

        Vector3 newOffset = Vector3.Slerp(currentOffset, targetOffset, moveSpeed * Time.deltaTime);
        newOffset.y = 0f;

        transform.position = player.position + newOffset;
        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
    }
}
