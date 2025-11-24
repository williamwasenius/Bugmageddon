using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    [Header("Footsteps")]
    public GameObject footprintPrefab;
    public Transform leftFootSpawnPoint;
    public Transform rightFootSpawnPoint;

    private void SpawnFootprint(Transform foot)
    {
        Vector3 pos = foot.position;

        Quaternion rot = foot.rotation;
        Vector3 euler = rot.eulerAngles;
        rot = Quaternion.Euler(0f, euler.y, 0f);

        Instantiate(footprintPrefab, pos, rot);
    }

    public void LeftFootStep() => SpawnFootprint(leftFootSpawnPoint);
    public void RightFootStep() => SpawnFootprint(rightFootSpawnPoint);
}
