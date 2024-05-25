using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float crouchScale = 0.5f;
    private Rigidbody rb;
    private Vector2 movementInput;
    private bool isGrounded;
    private bool isDashing;
    private bool isCrouching;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        isDashing = false;
        isCrouching = false;
    }

    private void Update()
    {
        if (!isDashing)
        {
            // Apply movement in the Update method for smoother input response
            Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
        }

        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }

        if (!isDashing && Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            Dash();
        }

        if (!isDashing && Keyboard.current.leftCtrlKey.isPressed)
        {
            if (CrouchSettings.isToggleCrouchEnabled)
            {
                ToggleCrouch();
            }
            else
            {
                HoldCrouch();
            }
        }
        else
        {
            StopCrouch();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void Dash()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        Vector3 dashDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
        Vector3 dashVelocity = dashDirection * dashDistance / dashDuration;

        rb.velocity = dashVelocity;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
        isDashing = false;
    }

    private void ToggleCrouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            transform.localScale = new Vector3(1f, crouchScale, 1f);
        }
        else
        {
            isCrouching = false;
            transform.localScale = Vector3.one;
        }
    }

    private void HoldCrouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            transform.localScale = new Vector3(1f, crouchScale, 1f);
        }
    }

    private void StopCrouch()
    {
        if (isCrouching)
        {
            isCrouching = false;
            transform.localScale = Vector3.one;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void OnMove(InputValue movementValue)
    {
        // Get movement input value from the Input System
        movementInput = movementValue.Get<Vector2>();
    }
}