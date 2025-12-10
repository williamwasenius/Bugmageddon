using UnityEngine;
using UnityEngine.UIElements;

public class PylonIndicatorScript : MonoBehaviour
{
    public float correctionAngle = -77.5f;
    public Transform player;  
    public Transform indicatorImagePivot; 
    public Transform targetObject;
    public Camera mainCamera;
    public Mission4Script mission;
    private int currentInt = 0;

    public void Start()
    {
        mission = Mission4Script.Instance;
        targetObject = mission.sensorPods[currentInt].transform;
    }

    private void Update()
    {

        if (mission.sensorPods[currentInt].GetComponent<PylonCharge>().charged)
        {
            if (currentInt > mission.sensorPods.Length)
            {
                gameObject.SetActive(false);
            }
            else
            {
                currentInt++;
                targetObject = mission.sensorPods[currentInt].transform;
            }
        }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(player.position);
        indicatorImagePivot.position = screenPos;

        if (player == null || indicatorImagePivot == null || targetObject == null)
            return;

        Vector3 direction = targetObject.position - player.position;
        direction.y = 0; 

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        indicatorImagePivot.localEulerAngles = new Vector3(0f, 0f, angle - correctionAngle);
    }
}
