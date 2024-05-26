using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Keybindings keyBindings;
    private AnimatorManager animatorManager;
    private PlayerMovementController playerMovementController;

    [SerializeField] private GameObject character;

    [Header("Monitor input values (do not edit)")]
    [Header("Main 2D Axis")]
    public Vector2 movementInput;
    public Vector2 cameraInput;

    [Header("Animation Velocity")]
    public float moveAmount;
    public int animationVelocityModifier = 2; // 0 = idle | 1 = walk | 2 = run | 3 = sprint

    [Header("Dashing (Sprinting)")]
    public float dashingTimeRemaining = 2f;
    public bool dashingInProgress = false;

    [Header("Extras")]
    public bool isGamePaused = false;

    private void Awake()
    {
        DisplayCursor(false);

        keyBindings = new Keybindings();
        keyBindings.Enable();

        animatorManager = character.GetComponent<AnimatorManager>();
        playerMovementController = character.GetComponent<PlayerMovementController>();

        // Input events subscriptions
        keyBindings.Player.Fire.performed += playerMovementController.Fire;
        keyBindings.Player.Dash.performed += Dash;
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
                animationVelocityModifier = 3;

                dashingTimeRemaining -= Time.deltaTime;

                float currentTime = dashingTimeRemaining;
                currentTime += 1f;
                float secs = Mathf.Floor(currentTime % 60);
                //Debug.Log("seconds: " + secs);
            } else
            {
                ResetDash();
            }
        }

        moveAmount = Mathf.Clamp01(Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y)) / 3; // divide by 3 to separate the animations timings
        moveAmount *= animationVelocityModifier;

        if (moveAmount == 0)
        {
            ResetDash();
        }

        animatorManager.UpdateAnimatorValues(moveAmount);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        //Debug.Log("dash performed");
        dashingInProgress = true;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            DisplayCursor(true);
        } else
        {
            Time.timeScale = 1f;
            DisplayCursor(false);
        }
    }

    // Helper functions

    private void ResetDash()
    {
        animationVelocityModifier = 2;
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
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}