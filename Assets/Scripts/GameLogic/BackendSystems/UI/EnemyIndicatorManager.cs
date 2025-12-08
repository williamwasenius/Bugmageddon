using UnityEngine;

public class EnemyIndicatorManager : MonoBehaviour
{
    /*public float correctionAngle;
    public Transform player;  
    public Transform indicatorImagePivot; 
    public Transform targetObject;
    public Camera mainCamera;

    private void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(player.position);
        indicatorImagePivot.position = screenPos;

        if (player == null || indicatorImagePivot == null || targetObject == null)
            return;

        Vector3 direction = targetObject.position - player.position;
        direction.y = 0; 

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        indicatorImagePivot.localEulerAngles = new Vector3(0f, 0f, angle - correctionAngle);
    }*/
}
