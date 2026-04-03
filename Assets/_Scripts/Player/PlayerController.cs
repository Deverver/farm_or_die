using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] private float Speed { get; set; } = 2f;

    private Rigidbody2D rb;
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2();

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
            animator.SetInteger("Direction", 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
            animator.SetInteger("Direction", 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            animator.SetInteger("Direction", 2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            animator.SetInteger("Direction", 3);
        }
        
        direction.Normalize();

        animator.SetBool("IsMoving", direction.magnitude > 0);
        rb.linearVelocity = Speed * direction;
    }
}
