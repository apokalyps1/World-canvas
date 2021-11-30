using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    public Sprite[] standing;
    public Sprite[] crouching;
    private BoxCollider2D collide2D;
    public Animator animator;
    public float jumpForce = 0.2f;
    bool isGrounded;
    public Transform groundCheck;
    public Transform wallcheck;
    public LayerMask groundlayer;
    public Rigidbody2D rb;
    public float move = 0f;
    public float speed = 20f;
    public SpriteRenderer sp;
    bool doublejump;
    bool isAtWall;
    bool walljump;
    public float walljumprepulsion = 20f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(move));
        if (Input.GetKeyDown("w"))
        {
            if (isGrounded)
            {
                Jump();  // script to jump

                doublejump = true;
                walljump = true;
            }
            else if (doublejump)
            {
                Jump();  // script to double jump

                doublejump = false;
            }
            else if (isAtWall)
            {
                if (walljump)  // script that implements wall jumping
                {
                    Jump();
                    rb.velocity = new Vector2(-move, rb.velocity.y);
                    walljump = false;
                    
                }
            }
        }
    }

    void FixedUpdate()
    {

        
        FlipPlayer();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
        movement_sides();
        isAtWall = Physics2D.OverlapCircle(wallcheck.position, 1f, groundlayer);
    }

    void movement_sides()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15;
        }
        else
        {
            speed = 10;
        }
        rb.velocity = new Vector2(move, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void FlipPlayer()
    {
        if (move > 0.1f)
        {
            sp.flipX = false;
        }
        else if ( move < -0.1f)
        {
            sp.flipX = true;
        }
    }       
}