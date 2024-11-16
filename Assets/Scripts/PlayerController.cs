using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Rotation
    private float mouseSensitivity = 100f;
    private float yRotation = 0f; 
      
    // Movement
    private float speed;
    private float crouchSpeed = 3f;
    private float walkSpeed = 5f;
    private float jogSpeed = 8f;

    // Bools
    private bool isCrouching = false;
    private bool isJogging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Ensures there is no stuttering when colliding with objects
    void FixedUpdate()
    {
        // Stores the values of the user's inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       
        // Rotation
        yRotation += mouseX;      
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
       
        // Movement
        // Checks to see if the player is jogging or crouching
        if (Input.GetKey(KeyCode.C) == true)
            isCrouching = true;
        else if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            isJogging = true;
            isCrouching = false;
        }
        else if (!Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.LeftShift))
        {
            isCrouching = false;
            isJogging = false;
        }

        // Sets the speed based on whether the player is jogging or crouching
        speed = isCrouching ? crouchSpeed : isJogging ? jogSpeed : walkSpeed;

        // Normalizes the movement so moving diagonal isn't faster than non-diagonal movements
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // Moves the player
        transform.Translate(speed * Time.deltaTime * movement);
    }
}
