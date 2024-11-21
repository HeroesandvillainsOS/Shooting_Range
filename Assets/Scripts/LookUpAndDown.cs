using UnityEngine.InputSystem;
using UnityEngine;

public class LookUpAndDown : MonoBehaviour
{
    // Rotation
    private float mouseSensitivity = 0.1f;
    private float xRotation = 0f;
    private float maxRotation = 45f;

    // Crouching variables
    //private float standHeight = 1.7f;
    //private float crouchHeight = 1.3f;
    private float smoothTransitionTime = 10f;

    // Input System
    private Vector2 lookDirection;
    public InputActionReference look;

    // Reference for the PlayerController script
    private PlayerController playerController;


    private void Start()
    {
        // Creates an object of the PlayerController component
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        // Reads the Input System moveDirection Vector 2 value
        lookDirection = look.action.ReadValue<Vector2>();

        // Gets the player object's height
        float playerHeight = transform.parent.position.y;
        float heightDifference = playerHeight - transform.position.y; // 1.7, 1.0 = .07
        float standHeight = playerHeight - heightDifference;
        Debug.Log($"Height Diffence {heightDifference} Stand Height {standHeight}");
        float crouchHeight = (playerHeight - heightDifference) - 0.1f;

        // Sets the crouch height and ensures a smooth transition
        Vector3 currentPosition = transform.position;
        float targetHeight = playerController.isCrouching ? crouchHeight : standHeight;
        float smoothHeight = Mathf.Lerp(transform.position.y, targetHeight, Time.deltaTime * smoothTransitionTime);
        transform.position = new Vector3(currentPosition.x, smoothHeight, currentPosition.z);
    }

    // Ensures vertical rotation is calculated after horizontal rotation
    void LateUpdate()
    {
        float mouseY = lookDirection.y * mouseSensitivity;

        // Inverts the vertical mouse movements
        xRotation -= mouseY;
        // Limits vertical rotation
        xRotation = Mathf.Clamp(xRotation, -maxRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}

