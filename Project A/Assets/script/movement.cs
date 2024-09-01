using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{

    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        wallGrab = coll.onWall && Input.GetKey(KeyCode.LeftShift);
        
        if (wallGrab)
        {
            rb.velocity = new Vector2(rb.velocity.x, y * speed);

        }

        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (coll.onGround)
                Jump();
            if (coll.onWall && !coll.onGround)
            {
                //wallJump();
            }
                
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Dash(xRaw, yRaw);
        }
    }

    private void Dash(float x, float y)
    {
        wallJumped = true;
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * 30;

    }

    private void WallJump()
    {
        //StopCoroutine(DisableMovement(0));
        //StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
    }
    private void WallSlide()
    {
        if (!canMove)
            return;

        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {

        if (!canMove)
            return;
        if (!wallJumped)
        {
            rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), .5f * Time.deltaTime);
        }
        
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;

    }
}