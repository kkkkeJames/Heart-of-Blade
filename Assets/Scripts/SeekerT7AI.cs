using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class SeekerT7AI : Enemy
{
    private Rigidbody2D rb;
    private CapsuleCollider2D myCapsule;
    private BoxCollider2D myFeet;

    [SerializeField] private float jumpTime = 2f;
    [SerializeField] private float jumpSpeed = 3f;
    private bool isOnGround;

    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myCapsule = GetComponent<CapsuleCollider2D>();
        health = 200;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        isOnGroundCheck();
        if (isOnGround)
            jumpTime = 2f;
        if (playerTransform.position.x >= transform.position.x)
        rb.velocity = new Vector2(6f, rb.velocity.y);
        else rb.velocity = new Vector2(-6f, rb.velocity.y);

        if (math.abs(playerTransform.position.x - transform.position.x) <= 2f && playerTransform.position.y > transform.position.y)
        {
            if (jumpTime > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpTime -= Time.deltaTime;
            }
        }
    }
    void isOnGroundCheck()
    {
        if (myFeet.IsTouchingLayers(groundMask))
        {
            isOnGround = true;
        }
        else isOnGround = false;
    }
}
