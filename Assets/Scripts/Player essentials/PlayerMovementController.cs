using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private float rotationSpeed = 15.0f;

    private InputManager inputManager;
    private CameraManager cameraManager;
    private Transform cameraTransform;
    private Vector3 movementDirection;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        playerRigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    // To move the actual character using RigidBody (physics)
    private void FixedUpdate()
    {
        HandleAllMovement();
    }

    private void LateUpdate()
    {
        if (Time.timeScale != 0.0f) cameraManager.HandleAllCameraMovement();
    }

    // Player Input Handling

    public void HandleAllMovement()
    {
        HandleMovement(true);
        HandleRotation(true);
    }

    private void HandleMovement(bool isActive)
    {
        movementDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z) * inputManager.verticalInput;
        movementDirection += cameraTransform.right * inputManager.horizontalInput;

        if (!isActive) movementDirection = new Vector3(0, 0, 0);

        movementDirection.Normalize();
        movementDirection.y = 0;

        movementDirection *= GetPlayerSpeed();

        Vector3 movementVelocity = movementDirection;
        movementVelocity.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation(bool isActive)
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraTransform.forward * inputManager.verticalInput;
        targetDirection += cameraTransform.right * inputManager.horizontalInput;

        if (!isActive) targetDirection = new Vector3(0, 0, 0);

        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    // Extra function to return run speed (function might expand in future to include walk or sprint modes if needed)
    private float GetPlayerSpeed()
    {
        float speed = 0.0f;

        if (inputManager.moveAmount > 0.0f) { speed = runSpeed; }

        // Debug.Log($"Setting speed to: {speed}...");
        return speed;
    }
}