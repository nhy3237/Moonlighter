using UnityEngine;

public class RollingState : State<PlayerController>
{
    private Animator animator;
    private Vector2 movement;

    public RollingState(Animator animator, Vector2 movement)
    {
        this.animator = animator;
        this.movement = movement;
    }

    public override void Enter(PlayerController entity)
    {
        //animator.SetBool("IsMoving", true);
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("RollingTrigger");
    }

    public override void Execute(PlayerController entity)
    {
        Vector2 moveVelocity = movement.normalized * entity.rollSpeed;

        entity.GetComponentInParent<Rigidbody2D>().velocity = moveVelocity;
    }

    public override void Exit(PlayerController entity)
    {
        animator.SetBool("IsMoving", false);
    }
}
