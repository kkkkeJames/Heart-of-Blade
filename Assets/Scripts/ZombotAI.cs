using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ZombotAI : Enemy
{
    private Rigidbody2D rb;
    private BoxCollider2D myFeet;
    private Animator anim;

    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float jumpTime = 3f;
    [SerializeField] private float jumpSpeed = 2f;
    private bool isOnGround;

    public bool isAttack = false;

    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        isOnGroundCheck();
        if (isOnGround)
            jumpTime = 2f;
        Attack();
        if (math.abs(playerTransform.transform.position.x - transform.position.x) > 1 && !isAttack)
        {
            if (playerTransform.transform.position.x >= transform.position.x)
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            else rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

        if (math.abs(playerTransform.position.x - transform.position.x) <= 2f && playerTransform.position.y > transform.position.y && !isAttack)
        {
            if (jumpTime > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpTime -= Time.deltaTime;
            }
        }
    }
    void Attack()
    {
        if (math.abs(playerTransform.transform.position.x - transform.position.x) <= 1)
        {
            anim.SetBool("attack", true);
        }
        else anim.SetBool("attack", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().GetHit(10, 0.2f);
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
