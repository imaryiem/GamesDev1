using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private float cameraSensitivity = 15.0f;

    private InputManager im;
    private CameraManager cm;

    private Transform mainCameraTransform;
    private Vector3 movementDirection;
    private Rigidbody characterRigidbody;

    private void Awake()
    {
        mainCameraTransform = Camera.main.transform;
        characterRigidbody = GetComponent<Rigidbody>();
        im = FindObjectOfType<InputManager>();
        cm = FindObjectOfType<CameraManager>();
    }

    // To move the actual character using RigidBody (physics)
    private void FixedUpdate()
    {
        HandleAllMovement();
    }

    private void LateUpdate()
    {
        if (Time.timeScale != 0.0f) cm.HandleAllCameraMovement();
    }

    // Player Input Handling

    public void HandleAllMovement()
    {
        HandleMovement(true);
        HandleRotation(true);
    }

    private void HandleMovement(bool isActive)
    {
        movementDirection = new Vector3(mainCameraTransform.forward.x, 0, mainCameraTransform.forward.z) * im.movementInput.y;
        movementDirection += mainCameraTransform.right * im.movementInput.x;

        if (!isActive) movementDirection = new Vector3(0, 0, 0);

        movementDirection.Normalize();
        movementDirection.y = 0;

        movementDirection *= GetPlayerSpeed();

        Vector3 movementVelocity = movementDirection;
        movementVelocity.y = characterRigidbody.velocity.y;
        characterRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation(bool isActive)
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = mainCameraTransform.forward * im.movementInput.y;
        targetDirection += mainCameraTransform.right * im.movementInput.x;

        if (!isActive) targetDirection = new Vector3(0, 0, 0);

        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSensitivity * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    // Trigged from the Input Manager class
    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("firing!");
    }

    // Extra function to return run speed (function might expand in future to include walk or sprint modes if needed)
    private float GetPlayerSpeed()
    {
        float speed = 0.0f;

        if (im.moveAmount > 0.0f) { speed = runSpeed; }

        // Debug.Log($"Setting speed to: {speed}...");
        return speed;
    }
}