using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsScript : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetMusicLevel(float sliderLevel)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderLevel) * 20);
    }
}
