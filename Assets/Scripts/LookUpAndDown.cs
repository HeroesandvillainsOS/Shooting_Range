using UnityEngine.InputSystem;
using UnityEngine;

public class LookUpAndDown : MonoBehaviour
{
    // Rotation
    private float mouseSensitivity = 0.1f;
    private float xRotation = 0f;
    private float maxRotation = 45f;

    // Input System
    private Vector2 lookDirection;
    public InputActionReference look;

    private void Update()
    {
        // Reads the Input System moveDirection Vector 2 value
        lookDirection = look.action.ReadValue<Vector2>();
    }

    // Ensures vertical rotation is calculated after horizontal rotation
    void LateUpdate()
    {
        float mouseY = lookDirection.y * mouseSensitivity;

        // Inverts the vertical mouse movements
        xRotation -= mouseY;
        // Limits vertical rotation
        xRotation = Mathf.Clamp(xRotation, -maxRotation, maxRotation);
        // Initiates vertical rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}

