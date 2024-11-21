using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Rotation
    private float mouseSensitivity = 0.2f;
    private float yRotation = 0f; 
      
    // Movement
    private float speed;
    private float crouchSpeed = 3f;
    private float walkSpeed = 5f;
    private float jogSpeed = 8f;

    // Bools
    public bool isCrouching = false;
    private bool isJogging = false;

    // Input System
    private Vector2 moveDirection;
    private Vector2 lookDirection;
    // Assign controls in the Inspector
    public InputActionReference move;
    public InputActionReference look;
    public InputActionReference jog;
    public InputActionReference crouch;

    private void OnEnable()
    {
        // Subscribes to Button event methods
        jog.action.started += Jog;
        jog.action.canceled += StopJog;
        crouch.action.started += Crouch;
    }

    private void OnDisable()
    {
        // Unsubscribes to the Button event methods
        jog.action.started -= Jog;
        jog.action.canceled -= StopJog;
        crouch.action.started -= Crouch;
    }

    private void Update()
    {
        // Reads the Input System moveDirection Vector 2 value
        moveDirection = move.action.ReadValue<Vector2>();
        lookDirection = look.action.ReadValue<Vector2>();
    }

    // Ensures there is no stuttering when colliding with objects
    void FixedUpdate()
    {
        // Stores the values of the user's inputs     
        float mouseX = lookDirection.x * mouseSensitivity;
       
        // Rotation
        yRotation += mouseX;      
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
       
        // Sets the speed based on whether the player is jogging or crouching
        speed = isCrouching ? crouchSpeed : isJogging ? jogSpeed : walkSpeed;

        // Normalizes the movement so moving diagonal isn't faster than non-diagonal movements
        Vector3 movement = new Vector3(moveDirection.x, 0f, moveDirection.y).normalized;

        // Moves the player
        transform.Translate(speed * Time.deltaTime * movement);
    }

    // Methods that handle button events
    private void Jog(InputAction.CallbackContext context)
    {
        isJogging = true;
    }

    private void StopJog(InputAction.CallbackContext context)
    {
        isJogging = false;
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        isCrouching = !isCrouching;
    }
}
