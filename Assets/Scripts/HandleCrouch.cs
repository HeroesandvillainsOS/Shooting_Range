using UnityEngine;

public class HandleCrouch : MonoBehaviour
{
    // Crouching
    private float cameraStandHeight = 0.7f;
    private float cameraCrouchHeight = 0.4f;
    private float capsuleStandHeight = 2f;
    private float capsuleCrouchHeight = 1.5f;

    // Lerp smoothness
    private float smoothAmount = 1f;

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
            // Smoothly lerp the camera position to crouch height
            float cameraCrouchLerp = Mathf.Lerp(cameraStandHeight, cameraCrouchHeight, smoothAmount);
            transform.localPosition = new Vector3(currentPosition.x, cameraCrouchLerp , currentPosition.z);

            // Smoothly lerp the capsule collider height to crouch height
            capsuleCollider.height = Mathf.Lerp(capsuleStandHeight, capsuleCrouchHeight, smoothAmount);

        }
        // Handles standing
        else
        {
            // Smoothly lerp the camera position to stand height
            float cameraStandLerp = Mathf.Lerp(cameraCrouchHeight, cameraStandHeight, smoothAmount);
            transform.localPosition = new Vector3(currentPosition.x, cameraStandLerp, currentPosition.z);

            // Smoothly lerp the capsule collider height to stand height
            capsuleCollider.height = Mathf.Lerp(capsuleCrouchHeight, capsuleStandHeight, smoothAmount);
        }
    }
}
