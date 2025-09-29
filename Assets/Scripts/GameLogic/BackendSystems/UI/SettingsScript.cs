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
    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0 )
        {
            vSyncToggle.isOn = false;
        }
        else
        {
            vSyncToggle.isOn = true;
        }

        bool foundResolution = false;
        for( int i = 0; i < resolutions.Count; i++ )
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundResolution = true;
                selectedResolution = i;

                UpdateResolutionText();
            }
        }

        if( !foundResolution )
        {
            ResolutionType newResolution = new ResolutionType();
            newResolution.horizontal = Screen.width;
            newResolution.vertical = Screen.height;

            resolutions.Add( newResolution );
            selectedResolution = resolutions.Count - 1;
            UpdateResolutionText();
        }
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
        //Screen.fullScreen = fullScreenToggle.isOn;

        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;

        }else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullScreenToggle.isOn);
    }
}

[System.Serializable]
public class ResolutionType
{
    public int  horizontal, vertical;
}
