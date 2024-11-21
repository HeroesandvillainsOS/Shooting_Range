using UnityEngine;

public class HandleCrouch : MonoBehaviour
{
    // Crouching
    private float cameraStandHeight = 0.9f;
    private float cameraCrouchHeight = 0.4f;
    private float capsuleStandHeight = 2f;
    private float capsuleCrouchHeight = 1.5f;

    // Lerping
    private float smoothAmount = 0f;
    private float smoothTransitionSpeed = 10f;

    // Game Objects
    private PlayerController playerController;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        // Gets reference to Game Object Components
        playerController = GetComponentInParent<PlayerController>();
        capsuleCollider = GetComponentInParent<CapsuleCollider>();
    }

    void Update()
    {
        Vector3 currentPosition = transform.localPosition;

        // Handles crouching
        if (playerController.isCrouching)
        {
            smoothAmount += smoothTransitionSpeed * Time.deltaTime; // Clamps to 1f
        }

        // Handles standing
        else
        {
           smoothAmount -= smoothTransitionSpeed * Time.deltaTime; // Clamps to 0f
        }

        // Ensures smoothAmount (the "t" in Lerp(a, b, t)) is never less than 0 or more than 1
        smoothAmount = Mathf.Clamp01(smoothAmount);
       
        // When Lerp(a, b, t) t = 0, the currentCameraHeight = cameraStandHeight
        // When Lerp(a, b, t) t = 1, the currentCameraHeight = cameraCrouchHeight
        float currentCameraHeight = Mathf.Lerp(cameraStandHeight, cameraCrouchHeight, smoothAmount);
        float currentCapsuleHeight = Mathf.Lerp(capsuleStandHeight, capsuleCrouchHeight, smoothAmount);

        // Sets the Camera and Collider positions
        transform.localPosition = new Vector3(currentPosition.x, currentCameraHeight, currentPosition.z);
        capsuleCollider.height = currentCapsuleHeight;
    }
}
