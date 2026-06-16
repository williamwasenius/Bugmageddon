using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class SettingsScript : MonoBehaviour
{
    public Toggle fullScreenToggle, vSyncToggle;

    public List<ResolutionType> resolutions = new List<ResolutionType>();
    private int selectedResolution;

    public TMP_Text resolutionText;

    public Slider audioSlider;
    public Slider musicSlider;
    public Slider vFXSlider;
    void Start()
    {
        fullScreenToggle.isOn = SaveManager.Instance.fullScreen;
        vSyncToggle.isOn = SaveManager.Instance.vSync;
        selectedResolution = SaveManager.Instance.resolutionIndex;
        UpdateResolutionText();
        SetAudioLevel();
        ApplySavedGraphics();
    }

    void Update()
    {
        
    }
    public void ResolutionLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResolutionText();
    }
    public void ResolutionRight()
    {
        selectedResolution++;
        if(selectedResolution > (resolutions.Count - 1))
        {
            selectedResolution = (resolutions.Count - 1);
        }

        UpdateResolutionText();
    }

    public void UpdateResolutionText()
    {
        resolutionText.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }
    public void ApplyGraphicsButton()
    {
        SaveManager.Instance.fullScreen = fullScreenToggle.isOn;
        SaveManager.Instance.vSync = vSyncToggle.isOn;
        SaveManager.Instance.resolutionIndex = selectedResolution;

        if (vSyncToggle.isOn)
            {
                QualitySettings.vSyncCount = 1;
            }
        else
            {
                QualitySettings.vSyncCount = 0;
            }

        FullScreenMode mode = fullScreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, mode);

        SaveManager.Instance.SavePlayerData();
    }
    public void SetAudioLevel()
    {
        audioSlider.value = SaveManager.Instance.volumeSliderAmount;
        musicSlider.value = SaveManager.Instance.musicSliderAmount;
        vFXSlider.value = SaveManager.Instance.vFXSliderAmount;

    }
    private void ApplySavedGraphics()
    {
        QualitySettings.vSyncCount = SaveManager.Instance.vSync ? 1 : 0;
        FullScreenMode mode = SaveManager.Instance.fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(resolutions[SaveManager.Instance.resolutionIndex].horizontal, resolutions[SaveManager.Instance.resolutionIndex].vertical, mode);
    }
}

[System.Serializable]
public class ResolutionType
{
    public int  horizontal, vertical;
}
