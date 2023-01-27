using UnityEngine;
using UnityEngine.InputSystem;

public class NonVRInputManager : MonoBehaviour
{
    public PlayerInput playerInput;

    void Start()
    {
        EnableUIInputs();
    }

    void EnableGameInputs()
    {
        Debug.Log("EnableGameInputs");
        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("Player");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void EnableUIInputs()
    {
        Debug.Log("EnableUIInputs");
        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("UI");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Cursor.lockState != CursorLockMode.Locked)
        {
            EnableGameInputs();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState != CursorLockMode.None)
        {
            EnableUIInputs();
        }
    }
}
