using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private int velocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        velocity = Animator.StringToHash("velocity");
    }

    public void UpdateAnimatorValues(float velocity /*, bool isSprinting, bool isWalking*/)
    {
        animator.SetFloat(this.velocity, velocity, 0.1f, Time.deltaTime);
    }

    public void FootStepsSound()
    {
        //AudioManager.instance.Play("Steps");
    }
}