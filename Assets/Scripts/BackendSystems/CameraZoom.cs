using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineCamera cinemachineCam;
    public Vector3 startOffset = new Vector3(0, 50f, -22.5f);
    public Vector3 endOffset = new Vector3(0, 100f, -45f);
    public float transitionSpeed = 1f;

    private CinemachineFollow transposer;
    private float t = 0f;
    private bool transitioning = false;

    void Start()
    {
        transposer = cinemachineCam.GetComponent<CinemachineFollow>();
        transposer.FollowOffset = startOffset;
    }

    void Update()
    {
        if (transitioning)
        {
            t += Time.deltaTime * transitionSpeed;
            transposer.FollowOffset = Vector3.Lerp(startOffset, endOffset, t);

            if (t >= 1f)
                transitioning = false;
        }

        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            StartTransition();
        }*/
    }

    public void StartTransition()
    {
        transitioning = true;
        t = 0f;
    }
}
