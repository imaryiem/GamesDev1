using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private int velocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        velocity = Animator.StringToHash("velocity");
    }

    private void Update()
    {
        if (animator.GetAnimatorTransitionInfo(0).IsName("Blend Tree -> Jump"))
        {
            Debug.Log("jump stopped");
            animator.SetBool("jumping", false);
        }
    }

    public void UpdateAnimatorValues(float velocity /*, bool isSprinting, bool isWalking*/)
    {
        animator.SetFloat(this.velocity, velocity, 0.1f, Time.deltaTime);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // TODO: jump
        Debug.Log("jump performed (WIP)");
        animator.SetBool("jumping", true);
    }

    public void FootStepsSound()
    {
        //AudioManager.instance.Play("Steps");
    }
}