using UnityEngine;
using UnityEngine.UI;

public class NonVRCameraSetup : MonoBehaviour
{
    public Camera spectatorCamera;
    private RawImage rawImage;

    void Start()
    {
        if (this.GetComponent<RectTransform>() == null)
        {
            Debug.LogError("Script must be attached to a UI element with a RectTransform component.");
        }

        rawImage = this.GetComponent<RawImage>();
        if (rawImage == null)
        {
            Debug.LogError("Script must be attached to a UI element with a RawImage component.");
        }

        OnRectTransformDimensionsChange();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (spectatorCamera != null && rawImage != null)
        {
            spectatorCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            rawImage.texture = spectatorCamera.targetTexture;
        }
    }
}
