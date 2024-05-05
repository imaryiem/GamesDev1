using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float crouchScale = 0.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isDashing;
    private bool isCrouching;
    private float originalScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation along all axes
        originalScale = transform.localScale.y;
    }

    private void Update()
    {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDashing)
        {
            StartCoroutine(Dash());
        }

        // Crouch
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isCrouching)
            {
                Crouch();
            }
            else
            {
                Uncrouch();
            }
        }
    }

    private void Crouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, originalScale * crouchScale, transform.localScale.z);
        isCrouching = true;
    }

    private void Uncrouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
        isCrouching = false;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float dashTime = 0f;
        Vector3 dashDirection = rb.velocity.normalized;

        while (dashTime < dashDuration)
        {
            rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            dashTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}
