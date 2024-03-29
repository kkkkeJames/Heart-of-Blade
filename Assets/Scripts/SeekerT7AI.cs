using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class SeekerT7AI : Enemy
{
    private Rigidbody2D rb;
    private CapsuleCollider2D myCapsule;
    private BoxCollider2D myFeet;
    private Animator anim;

    [SerializeField] private float jumpTime = 1.6f;
    [SerializeField] private float jumpSpeed = 4f;
    private bool isOnGround;

    public bool isAttack = false;
    public bool AttackEnd = false;
    public float AttackCD = 0.2f;

    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myCapsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        isOnGroundCheck();
        if (!Stunned)
        {
            if (isOnGround)
                jumpTime = 1.6f;
            Attack(); 
            if (math.abs(playerTransform.transform.position.x - transform.position.x) > 1.5f && !isAttack)
            {
                if (playerTransform.transform.position.x >= transform.position.x)
                {
                    direction = 1;
                    rb.velocity = new Vector2(6f, rb.velocity.y);
                }
                else
                {
                    direction = -1;
                    rb.velocity = new Vector2(-6f, rb.velocity.y);
                }
            }

            if (math.abs(playerTransform.position.x - transform.position.x) <= 2f && playerTransform.position.y > transform.position.y)
            {
                if (jumpTime > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    jumpTime -= Time.deltaTime;
                }
            }
        }
        if (Stunned)
        {
            rb.velocity = new Vector2(-StunTime * direction * 1f, rb.velocity.y);
        }
    }
    void Attack()
    {
        if (math.abs(playerTransform.transform.position.x - transform.position.x) <= 1.5f && math.abs(playerTransform.position.y - transform.position.y) <= 1f && AttackCD <= 0 && !isAttack)
        {
            direction = playerTransform.transform.position.x > transform.position.x ? 1 : -1;
            isAttack = true;
            anim.SetBool("attack", true);
        }
        if (isAttack)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (AttackEnd)
        {
            anim.SetBool("attack", false);
            isAttack = false;
            AttackCD = 0.1f;
        }
        if (AttackCD > 0) AttackCD -= Time.deltaTime;
        else AttackCD = 0;
        if (AttackCD == 0) AttackEnd = false;
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
