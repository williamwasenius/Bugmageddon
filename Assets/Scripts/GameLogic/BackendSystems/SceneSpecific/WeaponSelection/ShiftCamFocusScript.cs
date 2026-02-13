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
    }

    public CinemachineCamera cmCamera;
    public CameraPreset[] presets;

    public void Start()
    {
        moveCamera(0);
    }

    public void moveCamera(int indexNum)
    {
        CameraPreset preset = presets[indexNum];
        cmCamera.Follow = preset.waypoint;
        cmCamera.LookAt = preset.lookTarget;
    }

}
