using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCController : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    Animator animator;
    Rigidbody2D rigid;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

   

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector2(horizontal, vertical);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

    }

    private void FixedUpdate()
    {
        //Move();
    }

    private void Move()
    {
        //rigid.velocity = motionVector * speed;
        //Debug.Log(rigid.velocity);
    }
}
