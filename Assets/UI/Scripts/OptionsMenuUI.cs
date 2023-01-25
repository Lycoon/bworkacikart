using UnityEngine;

public class OptionsMenuUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        for (int i = Screen.resolutions.Length - 1; i > 0; i--)
        {
            Debug.Log(i);
            resolutionDropdown.options[i].text = Screen.resolutions[i].width + "x" + Screen.resolutions[i].height;
        }
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetFullscreenMode(int mode)
    {
        Screen.fullScreenMode = (FullScreenMode)(mode + 1);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
