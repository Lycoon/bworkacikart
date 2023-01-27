using UnityEngine;
using UnityEngine.InputSystem;

public class NonVRPlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float gravityModifier = 2.0f;
    public Transform cameraTransform;

    private CharacterController controller;
    private PlayerInput playerInput;

    private Vector2 moveInput = Vector2.zero;

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

    void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalSpeed = jumpForce;
        }
    }

    void Update()
    {
        Vector3 move = cameraTransform.forward * moveInput.y * moveSpeed;
        move += cameraTransform.right * moveInput.x * moveSpeed;

        verticalSpeed += Physics.gravity.y * gravityModifier * Time.deltaTime;
        move.y = verticalSpeed;

        controller.Move(move * Time.deltaTime);

        if (moveInput.magnitude > 0.0f)
        {
            Vector3 cameraForward = transform.position - cameraTransform.position;
            cameraForward.y = 0.0f;
            cameraForward = cameraForward.normalized;
            Vector3 cameraRight = Vector3.Cross(cameraForward, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-moveInput.x * cameraRight + moveInput.y * cameraForward), Time.deltaTime * rotationSpeed);
        }
    }
}
