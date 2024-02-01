using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("move Speed")]
    public float moveSpeed = 1f;
    [Header("Roll Speed")]
    public float rollSpeed = 3f;

    private float currentHorizontal = 0f;
    private float currentVertical = 0f;

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
            if(currentVertical == 0f && currentHorizontal == 0f)
            {
                movement = new Vector2(0, -1);
            }
            else
            {
                movement = new Vector2(currentHorizontal, currentVertical);
            }

            stateMachine.ChangeState(new RollingState(animator, movement));
        }
        else if(!animator.GetBool("IsRolling"))
        {
            if (movement.magnitude > 0)
            {
                currentHorizontal = horizontalInput;
                currentVertical = verticalInput;
                stateMachine.ChangeState(new MovingState(animator, movement));
            }
            else
            {
                stateMachine.ChangeState(new IdleState(animator));
            }
        }
    }
}
