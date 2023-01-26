using UnityEngine;

public class OptionsMenuUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetFullscreenMode(int mode)
    {
        Screen.fullScreenMode = (FullScreenMode)(mode + 1);
    }
}
