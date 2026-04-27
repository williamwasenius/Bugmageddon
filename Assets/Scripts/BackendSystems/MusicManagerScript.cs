using AudioSystem;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public bool autoPlayAmbient;
    public bool autoPlayMusic;

    public string introSource;
    public string loopSource;
    public string ambienceSource;

    void Start()
    {
        if (autoPlayAmbient)
        {
            PlayAmbience();
        }
        if (autoPlayMusic)
        {
            PlayMusic();
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
