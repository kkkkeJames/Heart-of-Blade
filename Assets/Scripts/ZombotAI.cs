using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ZombotAI : Enemy
{
    private Rigidbody2D rb;
    private BoxCollider2D myFeet;
    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float jumpTime = 3f;
    [SerializeField] private float jumpSpeed = 2f;
    private bool isOnGround;

    public bool isAttack = false;
    public float AttackTime = 0f;

    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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
        if (math.abs(playerTransform.transform.position.x - transform.position.x) > 1.5f && !isAttack)
        {
            if (playerTransform.transform.position.x >= transform.position.x)
            {
                sr.flipX = false;
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                sr.flipX = true;
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }

        /*
        if (math.abs(playerTransform.position.x - transform.position.x) <= 2f && playerTransform.position.y > transform.position.y && !isAttack)
        {
            if (jumpTime > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpTime -= Time.deltaTime;
            }
        }
        */
        if (isAttack)
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (AttackTime > 0)
        {
            AttackTime -= Time.deltaTime;
            if (AttackTime < 0) AttackTime = 0;
            if (!sr.flipX) rb.velocity = new Vector2(AttackTime * 1.2f, 0);
            else rb.velocity = new Vector2(-AttackTime * 1.4f, 0);
        }
    }
    void Attack()
    {
        if (math.abs(playerTransform.transform.position.x - transform.position.x) <= 1.5f)
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
