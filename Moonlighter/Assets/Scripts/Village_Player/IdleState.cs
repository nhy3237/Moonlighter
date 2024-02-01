using UnityEngine;

public class IdleState : State<PlayerController>
{
    private Animator animator;

    public IdleState(Animator animator)
    {
        this.animator = animator;
    }

    public override void Enter(PlayerController entity)
    {
        entity.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetBool("IsMoving", false);
    }

    public override void Execute(PlayerController entity)
    {
    }

    public override void Exit(PlayerController entity)
    {
    }
}
