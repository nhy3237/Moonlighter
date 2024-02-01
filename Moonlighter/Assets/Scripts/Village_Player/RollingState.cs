using UnityEngine;

public class RollingState : State<PlayerController>
{
    private Animator animator;
    private Vector2 movement;
    private float rollingDistance = 0.5f;
    private Vector2 startRollingPosition;

    public RollingState(Animator animator, Vector2 movement)
    {
        this.animator = animator;
        this.movement = movement;
    }

    public override void Enter(PlayerController entity)
    {
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsRolling", true);

        startRollingPosition = entity.transform.parent.position;
    }

    public override void Execute(PlayerController entity)
    {
        float currentDistance = Vector2.Distance(startRollingPosition, entity.transform.parent.position);

        if (currentDistance < rollingDistance)
        {
            Vector2 moveVelocity = movement.normalized * entity.rollSpeed;
            entity.GetComponentInParent<Rigidbody2D>().velocity = moveVelocity;
        }
        else
        {
            entity.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
            animator.SetBool("IsRolling", false);
        }
    }

    public override void Exit(PlayerController entity)
    {
    }
}
