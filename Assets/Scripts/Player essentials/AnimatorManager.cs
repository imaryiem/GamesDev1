using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private int velocity;
    private int jumpAnimation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        velocity = Animator.StringToHash("velocity");
        jumpAnimation = Animator.StringToHash("Jump");
    }

    public void UpdateAnimationVelocity(float velocity)
    {
        animator.SetFloat(this.velocity, velocity, 0.1f, Time.deltaTime);
    }

    public void PlayJumpAnimation()
    {
        animator.Play(jumpAnimation);
    }

    public void FootStepsSound()
    {
        //AudioManager.instance.Play("Steps");
    }
}