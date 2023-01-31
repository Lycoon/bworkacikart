using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NonVRInputManager : MonoBehaviour
{
    public enum InputMode { Game, UI }

    [Header("References")]
    public PlayerInput playerInput;
    public GameObject pauseMenu;
    public RawImage vrView;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu();
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
        vrView.enabled = !vrView.enabled;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
