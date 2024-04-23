using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private const string IdleState = "Idle";
    private const string IdleHappyState = "IdleHappy";
    private const string IdleScaredState = "IdleScared";
    private const string IdleTalkingState = "IdleTalking";
    private const string WalkState = "Walk";

    public void PlayIdle()
    {
        animator.Play(IdleState);
    }

    public void PlayIdleHappy()
    {
        animator.Play(IdleHappyState);
    }

    public void PlayIdleScared()
    {
        animator.Play(IdleScaredState);
    }

    public void PlayIdleTalking()
    {
        animator.Play(IdleTalkingState);
    }

    public void PlayWalk(float moveX, float moveY)
    {
        animator.SetBool(WalkState, true);
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
    }

    public void StopWalk()
    {
        animator.SetBool(WalkState, false);
    }
}
