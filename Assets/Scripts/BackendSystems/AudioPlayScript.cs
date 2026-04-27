using AudioSystem;
using UnityEngine;

public class AudioPlayScript : MonoBehaviour
{
    public AudioData[] audioClips;

    public void Play(int index)
    {
        if (index < 0 || index >= audioClips.Length || audioClips[index] == null)
            return;

        AudioManager.Instance.Play(audioClips[index].id, transform.position);
    }
}