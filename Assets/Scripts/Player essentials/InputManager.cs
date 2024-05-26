using UnityEngine;

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

    private void Awake()
    {        
        keyBindings = new Keybindings();
        keyBindings.Player.Enable();

        animatorManager = character.GetComponent<AnimatorManager>();
        playerMovementController = character.GetComponent<PlayerMovementController>();

        // Input events subscriptions
        keyBindings.Player.Fire.performed += playerMovementController.Fire;
    }

    // Update values so that other classes can read them
    private void Update()
    {
        movementInput = keyBindings.Player.Move.ReadValue<Vector2>();
        cameraInput = keyBindings.Player.Look.ReadValue<Vector2>();

        int speedModifier = 0; // 0 = walk | 1 = run | 2 = sprint

        if (keyBindings.Player.Move.IsInProgress()) speedModifier = 1; // TODO: should be adjusted later on

        moveAmount = (speedModifier + Mathf.Clamp01(Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y))) / 3; // divide by 3 to separate the animations timings
        animatorManager.UpdateAnimatorValues(moveAmount);
    }
}