using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ShiftCamFocusScript : MonoBehaviour
{
    [System.Serializable]
    public class CameraPreset
    {
        public Transform waypoint;
        public Transform lookTarget;
        public GameObject blockingObject;
    }

    public CinemachineCamera cmCamera;
    public CameraPreset[] presets;
    public int currentCamera = 0;

    public void Start()
    {
        moveCamera(currentCamera);
    }

    public void moveCamera(int indexNum)
    {
        CameraPreset preset = presets[currentCamera];

        if (preset.blockingObject != null)
        {
            preset.blockingObject.SetActive(true);
        }

        currentCamera = indexNum;
        preset = presets[indexNum];

        cmCamera.Follow = preset.waypoint;
        cmCamera.LookAt = preset.lookTarget;

        if (preset.blockingObject != null)
        {
            preset.blockingObject.SetActive(false);
        }
    }

}
