using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public bool autoPlay;

    public AudioSource introSource;
    public AudioSource loopSource;

    void Start()
    {
        if (autoPlay)
        StartCoroutine(PlayMusic());
    }

    public System.Collections.IEnumerator PlayMusic()
    {
        introSource.Play();

        yield return new WaitForSecondsRealtime(introSource.clip.length);

        loopSource.Play();
    }
}
