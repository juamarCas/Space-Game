﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;             //this is the speed our player will move
    private Rigidbody2D rb;         //variable to store Rigidbody2D component
    private float moveInput;        //this store the input value

    public float jumpForce;         //force with which player jump
    private bool isGrounded;        //this variable will tell if our player is grounded or not
    public Transform feetPos;       //this variable will store reference to transform from where we will create a circle
    public float circleRadius;      //radius of circle
    public LayerMask whatIsGround;  //layer our ground will have.

    public float jumpTime;          //time till which we will apply jump force
    private float jumpTimeCounter;  //time to count how long player has pressed jump key
    private bool isJumping;         //bool to tell if player is jumping or not

    private float velMultiplier = 1f;

    [Space]
    [Header("JetPack")]
    public float jetPackForce = 10f;
    public int jetPackJumps = 100000; //  cuantos jumps con jetpack se puede hacer

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //get reference to 	Rigidbody2D component
    }

    //as we are going to use physics for our player , we must use FixedUpdate for it
    void FixedUpdate()
    {
        Movement();
        Skills();
    }

    private void Update()
    {
        IsGrounded();
    }

    #region movement
    private void Movement()
    {
        VelMul();
        //the moveInput will be 1 when we press right key and -1 for left key
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0)                                  //moving towards right side
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveInput < 0)                             //moving towards left side
            transform.eulerAngles = new Vector3(0, 180, 0);
        //here we set our player x velocity and y will not ne changed
        rb.velocity = new Vector2(moveInput * speed * velMultiplier, rb.velocity.y);
    }

    private void IsGrounded()
    {
        //here we set the isGrounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        //we check if isGrounded is true and we pressed Space button
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;                           //we set isJumping to true
            jumpTimeCounter = jumpTime;                 //set jumpTimeCounter
            rb.velocity = Vector2.up * jumpForce;       //add velocity to player
        }

        //if Space key is pressed and isJumping is true
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)                    //we check if jumpTimeCounter is more than 0
            {
                rb.velocity = Vector2.up * jumpForce;   //add velocity
                jumpTimeCounter -= Time.deltaTime;      //reduce jumpTimeCounter by Time.deltaTime
            }
            else                                        //if jumpTimeCounter is less than 0
            {
                isJumping = false;                      //set isJumping to false
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))              //if we unpress the Space key
        {
            isJumping = false;                          //set isJumping to false
        }
    }
    // multiplicador de velocidad para correr
    private void VelMul()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velMultiplier = 1.5f;
        }
        else
        {
            velMultiplier = 1f;
        }
    }
    #endregion

    #region skills
    //Aquí se pondrán todas las habilidades del jugador
    private void Skills()
    {
        //Jetpack
        if(jetPackJumps > 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jetPackForce, ForceMode2D.Impulse);
                jetPackJumps--;
            }
        }
    }
    #endregion
}
