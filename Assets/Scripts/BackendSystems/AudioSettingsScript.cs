using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsScript : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerGroup SFX;
    public AudioMixerGroup Music;

    public void SetVolumeLevel(float sliderLevel)
    { 
        mixer.SetFloat("TotalVol", Mathf.Log10(sliderLevel) * 20);
        SaveManager.Instance.volumeSliderAmount = sliderLevel;
    }
    public void SetMusicLevel(float sliderLevel)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderLevel) * 20);
        SaveManager.Instance.musicSliderAmount = sliderLevel;
    }
    public void SetVFXLevel(float sliderLevel)
    {
        mixer.SetFloat("VFXVol", Mathf.Log10(sliderLevel) * 20);
        SaveManager.Instance.vFXSliderAmount = sliderLevel;
    }
}
