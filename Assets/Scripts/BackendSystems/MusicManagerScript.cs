using AudioSystem;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public bool autoPlay;

    public string introSource;
    public string loopSource;
    public string ambienceSource;

    void Start()
    {
        if (autoPlay)
        {
            PlayMusic();
            PlayAmbience();
        }
    }

    public void PlayMusic()
    {
        StartCoroutine(AudioManager.Instance.PlayMusic(introSource, loopSource));
    }
    public void PlayAmbience()
    {
        StartCoroutine(AudioManager.Instance.PlayMusic("",ambienceSource));
    }

}
