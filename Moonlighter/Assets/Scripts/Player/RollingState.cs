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
        animator.SetBool("IsMoving", true);
        animator.SetTrigger("RollTrigger");
    }

    public override void Execute(PlayerController entity)
    {
        float moveSpeed = entity.rollSpeed;
        Vector2 movementVector = movement.normalized;

        entity.GetComponent<Rigidbody2D>().velocity = movementVector * moveSpeed;
    }

    public override void Exit(PlayerController entity)
    {
        animator.SetBool("IsMoving", false);
    }
}
