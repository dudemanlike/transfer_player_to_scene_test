using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeedMultiplier = 2f;
    public float jumpForce = 10f;
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private bool isGrounded;
    private float verticalVelocity;
    private float mouseX;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Player Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection.Normalize();

        float currentMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
        controller.Move(moveDirection * currentMoveSpeed * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        // Gravity
        if (!isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
            controller.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));
        }

        // Camera Rotation
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.rotation = Quaternion.Euler(0, mouseX, 0);

        // Cursor Management
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;
    }
}
