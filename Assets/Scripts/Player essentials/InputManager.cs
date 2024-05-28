using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Keybindings keyBindings;
    private AnimatorManager am;

    [Header("Monitor input values (do not edit)")]
    [Header("Main 2D Axis")]
    public Vector2 movementInput;
    public Vector2 cameraInput;

    [Header("Animation Velocity")]
    public float moveAmount;
    public int animationSelector = 2; // 0 = idle | 1 = walk | 2 = run | 3 = sprint

    [Header("Jumping")]
    public bool jumpPressed = false;
    public bool isGrounded;

    [Header("Dashing (Sprinting)")]
    public float dashingTimeRemaining = 2f;
    public bool dashingInProgress = false;

    [Header("Extras")]
    public bool isGamePaused = false;

    [Header("Debugging")]
    public bool drawRaycasts = false;

    private void Awake()
    {
        DisplayCursor(false);

        keyBindings = new Keybindings();
        keyBindings.Enable();

        am = GetComponent<AnimatorManager>();

        // Input events subscriptions
        keyBindings.Player.Dash.performed += Dash;
        keyBindings.Player.Jump.performed += Jump;
        keyBindings.UI.Pause.performed += Pause;
    }

    // Update values so that other classes can read them
    private void Update()
    {
        movementInput = keyBindings.Player.Move.ReadValue<Vector2>();
        cameraInput = keyBindings.Player.Look.ReadValue<Vector2>();

        if (dashingInProgress)
        {
            if (dashingTimeRemaining > 0)
            {
                animationSelector = 3;

                dashingTimeRemaining -= Time.deltaTime;

                float currentTime = dashingTimeRemaining;
                currentTime += 1f;
                float secs = Mathf.Floor(currentTime % 60);
                //Debug.Log("seconds: " + secs);
            }
            else
            {
                ResetDash();
            }
        }

        moveAmount = Mathf.Clamp01(Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y)) / 3; // divide by 3 to split the animations timings
        moveAmount *= animationSelector;

        if (moveAmount == 0)
        {
            ResetDash();
        }

        am.UpdateAnimationVelocity(moveAmount);
    }

    private void FixedUpdate()
    {
        // Check grounded status (used for jump action)
        CheckGroundedStatus_RaycastSimple();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpPressed = true;
        am.PlayJumpAnimation();
    }

    private void Dash(InputAction.CallbackContext context)
    {
        dashingInProgress = true;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            DisplayCursor(true);
        }
        else
        {
            Time.timeScale = 1f;
            DisplayCursor(false);
        }
    }

    // Helper functions

    private void ResetDash()
    {
        animationSelector = 2;
        dashingTimeRemaining = 2f;
        dashingInProgress = false;
        //Debug.Log("dashing completed/ reset");
    }

    private void DisplayCursor(bool visibility)
    {
        if (visibility)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Ground checking methods
    private void CheckGroundedStatus_RaycastSimple()
    {
        float rayLength = 2f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength);
        if (drawRaycasts) Debug.DrawRay(transform.position, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
    }

    private void CheckGroundedStatus_RaycastImproved()
    {
        // Define a layer mask for the ground (ensure to set the ground layer correctly in the Inspector)
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // Perform raycasts from multiple points on the player's collider
        Vector3 origin = transform.position;
        float rayLength = 2f; // Adjust this value based on your player’s height
        float radius = 0.23f; // Adjust this value based on your player’s width

        // Check if the raycast hits the ground layer
        isGrounded = Physics.Raycast(origin, Vector3.down, rayLength, groundLayer) ||
                     Physics.Raycast(origin + Vector3.right * radius, Vector3.down, rayLength, groundLayer) ||
                     Physics.Raycast(origin - Vector3.right * radius, Vector3.down, rayLength, groundLayer) ||
                     Physics.Raycast(origin + Vector3.forward * radius, Vector3.down, rayLength, groundLayer) ||
                     Physics.Raycast(origin - Vector3.forward * radius, Vector3.down, rayLength, groundLayer);

        // Optional: Debug the raycasts in the scene view
        if (drawRaycasts)
        {
            Debug.DrawRay(origin, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
            Debug.DrawRay(origin + Vector3.right * radius, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
            Debug.DrawRay(origin - Vector3.right * radius, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
            Debug.DrawRay(origin + Vector3.forward * radius, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
            Debug.DrawRay(origin - Vector3.forward * radius, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
        }
    }

    private void CheckGroundedStatus_SphereCast()
    {
        // Define a layer mask for the ground
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // SphereCast parameters
        float rayLength = 2f; // Adjust based on your player’s height
        float sphereRadius = 0.23f; // Adjust based on your player’s width

        // Perform the SphereCast
        isGrounded = Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out RaycastHit hit, rayLength, groundLayer);

        // Optional: Debug the SphereCast in the scene view
        if (drawRaycasts) Debug.DrawRay(transform.position, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
    }

    private void CheckGroundedStatus_CapsuleCast()
    {
        // Define a layer mask for the ground
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // CapsuleCast parameters
        float capsuleHeight = 2f; // Adjust based on your player’s height
        float capsuleRadius = 0.23f; // Adjust based on your player’s width
        Vector3 point1 = transform.position + Vector3.up * (capsuleHeight / 2 - capsuleRadius);
        Vector3 point2 = transform.position + Vector3.down * (capsuleHeight / 2 - capsuleRadius);

        // Perform the CapsuleCast
        isGrounded = Physics.CapsuleCast(point1, point2, capsuleRadius, Vector3.down, out RaycastHit hit, capsuleHeight, groundLayer);

        // Optional: Debug the CapsuleCast in the scene view
        if (drawRaycasts) Debug.DrawRay(transform.position, Vector3.down * capsuleHeight, isGrounded ? Color.green : Color.red);
    }
}