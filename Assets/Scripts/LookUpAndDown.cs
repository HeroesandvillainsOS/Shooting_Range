using UnityEngine;

public class LookUpAndDown : MonoBehaviour
{
    private float mouseSensitivity = 50f;
    private float xRotation = 0f;
    private float maxRotation = 45f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Ensures vertical rotation is calculated after horizontal rotation
    void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Inverts the horizontal mouse movements
        xRotation -= mouseY;
        // Limits horizontal rotation
        xRotation = Mathf.Clamp(xRotation, -maxRotation, maxRotation);


        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
