using UnityEngine;

public class InputManager : MonoBehaviour
{
    private AnimatorManager animatorManager;
    private Keybindings keyBindings;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        keyBindings = new Keybindings();
        keyBindings.Enable();
    }

    private void Update()
    {
        movementInput = keyBindings.Player.Move.ReadValue<Vector2>();
        cameraInput = keyBindings.Player.Look.ReadValue<Vector2>();

        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(moveAmount);
    }
}