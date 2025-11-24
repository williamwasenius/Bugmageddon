using UnityEngine;

public class EggCluster : MonoBehaviour
{
    private GameObject[] containedBugs;

    public void releaseBugs()
    {
        foreach (GameObject bug in containedBugs)
        {
            if (!bug.activeInHierarchy)
            {
                bug.SetActive(true);
            }
        }
    }

}
