using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource walkingSteps;

    public void steps()
    {
        walkingSteps.Play();
    }
}
