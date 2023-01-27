using UnityEngine;
using UnityEngine.InputSystem;

public class NonVRPlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 30.0f;
    public float jumpForce = 10.0f;
    public float gravityModifier = 2.0f;

    private CharacterController controller;
    private PlayerInput playerInput;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;

    private float horizontalSpeed = 0.0f;
    private float verticalSpeed = 0.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalSpeed = jumpForce;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState != CursorLockMode.None)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(0, lookInput.x * rotationSpeed * Time.deltaTime, 0);
        }

        Vector3 move = transform.forward * moveInput.y * horizontalSpeed;
        move += transform.right * moveInput.x * horizontalSpeed;

        verticalSpeed += Physics.gravity.y * gravityModifier * Time.deltaTime;
        move.y = verticalSpeed;

        controller.Move(move * Time.deltaTime);

        if (controller.isGrounded)
        {
            horizontalSpeed = moveSpeed;
            verticalSpeed = 0.0f;
        }
    }
}
