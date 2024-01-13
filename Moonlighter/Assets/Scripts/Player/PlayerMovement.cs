using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("속도")]
    public float moveSpeed = 0.5f;

    private Animator animator;

    void Start()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move(horizontal, vertical);
        setAnimation(horizontal, vertical);
    }

    private void move(float horizontal, float vertical)
    {
        Vector2 movement = new Vector2(horizontal, vertical);
        Vector2 moveVelocity = movement.normalized * moveSpeed;

        GetComponentInParent<Rigidbody2D>().velocity = moveVelocity;
    }

    private void setAnimation(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            animator.SetBool("right", true);
            animator.SetBool("left", false);
        }
        else if (horizontal < 0)
        {
            animator.SetBool("left", true);
            animator.SetBool("right", false);
        }
        else
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
        }

        if (vertical > 0)
        {
            animator.SetBool("up", true);
            animator.SetBool("down", false);
        }
        else if (vertical < 0)
        {
            animator.SetBool("down", true);
            animator.SetBool("up", false);
        }
        else
        {
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }
    }
}
