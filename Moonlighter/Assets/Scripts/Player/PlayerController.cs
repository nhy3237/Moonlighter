using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("move Speed")]
    public float moveSpeed = 1f;
    [Header("Roll Speed")]
    public float rollSpeed = 3f;


    private StateMachine<PlayerController> stateMachine;
    private Animator animator;

    void Start()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();

        stateMachine = new StateMachine<PlayerController>();
        stateMachine.Setup(this, new IdleState(animator));
    }

    void Update()
    {
        stateMachine.Execute();

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(movement == Vector2.zero)
            {
                movement = new Vector2(0, -1);
            }

            stateMachine.ChangeState(new RollingState(animator, movement));
        }
        else
        {
            if (movement.magnitude > 0)
            {
                if (!animator.GetBool("IsRolling"))
                {
                    stateMachine.ChangeState(new MovingState(animator, movement));
                }
            }
            else
            {
                stateMachine.ChangeState(new IdleState(animator));
            }
        }
    }
}
