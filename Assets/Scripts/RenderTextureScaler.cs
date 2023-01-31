using UnityEngine;
using UnityEngine.UI;

public class RenderTextureScaler : MonoBehaviour
{
    public Camera renderCamera;

    private RectTransform rectTransform;
    private RawImage rawImage;

    private RenderTexture renderTexture;

    void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        if (rectTransform == null)
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

    void OnEnable()
    {
        OnRectTransformDimensionsChange();
    }

    void OnRectTransformDimensionsChange()
    {
        if (renderCamera != null && rectTransform != null)
        {
            renderTexture = new RenderTexture((int)(rectTransform.rect.width), (int)(rectTransform.rect.height), 24);
            rawImage.texture = renderTexture;
            renderCamera.targetTexture = renderTexture;
        }
    }
}
