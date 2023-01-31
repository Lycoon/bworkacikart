using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NonVRInputManager : MonoBehaviour
{
    public enum InputMode { Game, UI }
    private bool isFadingOut = false;

    [Header("References")]
    public PlayerInput playerInput;
    public GameObject pauseMenu;
    public RawImage VRView;
    public CanvasGroup blackScreenCanvasGroup;
    public GameObject blackScreen;
    public Slider brightnessSlider;
    public Light moon;
    public AudioSource tickAudioSource;

    [Header("Settings")]
    public bool isPaused = false;

    public void SetGameState(InputMode inputMode)
    {
        Debug.Log("Switching to " + inputMode + " input mode");
        switch (inputMode)
        {
            case InputMode.Game:
                playerInput.SwitchCurrentActionMap("Player");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case InputMode.UI:
                playerInput.SwitchCurrentActionMap("UI");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    void Start()
    {
        SetGameState(InputMode.Game);
        isFadingOut = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu();
        
        // Black screen fade out on scene loading
        if (isFadingOut)
        {
            blackScreenCanvasGroup.alpha -= Time.deltaTime * 0.2f;
            if (blackScreenCanvasGroup.alpha <= 0)
            {
                blackScreenCanvasGroup.alpha = 0;
                blackScreen.SetActive(false);
                isFadingOut = false;
            }
        }

        // Brightness value
        moon.intensity = brightnessSlider.value;
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back to main menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (!isPaused)
            SetGameState(InputMode.Game);
        else
            SetGameState(InputMode.UI);
    }

    public void ToggleOptions()
    {
        Debug.Log("Options");
    }

    public void ToggleVRView()
    {
        VRView.enabled = !VRView.enabled;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void PlayTickSound()
    {
        tickAudioSource.Play();
    }
}
