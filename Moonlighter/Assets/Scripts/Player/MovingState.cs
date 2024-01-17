using UnityEngine;

public class MovingState : State<PlayerController>
{
    private Animator animator;
    private Vector2 movement;

    public MovingState(Animator animator, Vector2 movement)
    {
        this.animator = animator;
        this.movement = movement;
    }

    public override void Enter(PlayerController entity)
    {
        animator.SetFloat("HorizontalBlend", movement.x);
        animator.SetFloat("VerticalBlend", movement.y);

        animator.SetBool("IsMoving", true);
    }

    public override void Execute(PlayerController entity)
    {
        Vector2 moveVelocity = movement.normalized * entity.moveSpeed;
        
        entity.GetComponentInParent<Rigidbody2D>().velocity = moveVelocity;
    }

    public override void Exit(PlayerController entity)
    {
        animator.SetBool("IsMoving", false);
    }
}
