using UnityEngine;
using UnityEngine.UIElements;

public class AudioPlayScript : MonoBehaviour
{
    public AudioClip[] audioClips;

    public void Play(int clipNumber,float volumeModifier)
    {
        AudioSource.PlayClipAtPoint(audioClips[clipNumber], Camera.main.transform.position, volumeModifier);
    }
}
