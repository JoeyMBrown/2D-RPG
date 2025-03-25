using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Gets the hash of these values, this allows us to only have
    // to change this line if we need to change this in the future.
    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");
    private readonly int moving = Animator.StringToHash("Moving");
    private readonly int dead = Animator.StringToHash("Dead");
    private readonly int revive = Animator.StringToHash("Revive");
    private readonly int attacking = Animator.StringToHash("Attacking");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDeadAnimation()
    {
        animator.SetTrigger(dead);
    }

    public void SetMoveBoolTransition(bool value)
    {
        animator.SetBool(moving, value);
    }

    public void SetMoveAnimation(Vector2 dir)
    {
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }

    public void SetAttackAnimation(bool value)
    {
        animator.SetBool(attacking, value);
    }

    // Sets the animation to the idle animation, while
    // triggering the revive trigger.
    public void ResetPlayer()
    {
        SetMoveAnimation(Vector2.down);
        animator.SetTrigger(revive);
    }
}
