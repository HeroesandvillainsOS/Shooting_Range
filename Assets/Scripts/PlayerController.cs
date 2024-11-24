using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Rotation
    private float mouseSensitivity = 0.2f;
    private float yRotation = 0f; 
      
    // Movement
    private float speed;
    private float crouchSpeed = 2f;
    private float walkSpeed = 4f;
    private float jogSpeed = 8f;

    // Bools
    private bool isWalking = true;
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

    // Animator
    private Animator animator;
    private float hInput;
    private float vInput;
    private float crouchLayerWeight = 0f;
    private float crouchLayerTransitionSpeed = 10f;
    private int crouchLayerIndex;

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

    private void Start()
    {
        // Gets references to Game Object Components and Animator information
        animator = GetComponentInChildren<Animator>();
        crouchLayerIndex = animator.GetLayerIndex("Crouch Layer");
    }

    private void Update()
    {
        // Reads the Input System moveDirection Vector 2 value
        moveDirection = move.action.ReadValue<Vector2>();
        lookDirection = look.action.ReadValue<Vector2>();

        // Handles Animator movement input values
        animator.SetFloat("hInput", hInput);
        animator.SetFloat("vInput", vInput);
      
        // Handles Animator walking logic
        animator.SetBool("isWalking", isWalking);
        if (isCrouching || isJogging)
            isWalking = false;
        else
            isWalking = true;

        // Handles Animator crouching logic
        if (isCrouching)
            // Increases layer weight
            crouchLayerWeight += Time.deltaTime * crouchLayerTransitionSpeed;
        else
            crouchLayerWeight -= Time.deltaTime * crouchLayerTransitionSpeed;
        // Clamps layer weight
        crouchLayerWeight = Mathf.Clamp01(crouchLayerWeight);
        // Sets layer weight
        animator.SetLayerWeight(crouchLayerIndex, crouchLayerWeight);
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

        // Sets Animator movement input values
        hInput = moveDirection.x;
        vInput = moveDirection.y;   

        // Moves the player
        transform.Translate(speed * Time.deltaTime * movement);
    }

    // Methods that handle button events
    private void Jog(InputAction.CallbackContext context)
    {
        isJogging = true;
        animator.SetBool("isJogging", true);
    }

    private void StopJog(InputAction.CallbackContext context)
    {
        isJogging = false;
        animator.SetBool("isJogging", false);
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        isCrouching = !isCrouching;
    }
}
