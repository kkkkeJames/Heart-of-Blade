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
    public bool isInAttack = false;
    public float InAttackTime = 0.3f;
    public bool AttackEnd = false;
    public float AttackCD = 0.3f;

    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        isOnGroundCheck();
        if (!Stunned)
        {
            if (isOnGround)
                jumpTime = 2f;
            Attack();
            if (math.abs(playerTransform.transform.position.x - transform.position.x) > 1.5f && !isAttack)
            {
                if (playerTransform.transform.position.x >= transform.position.x)
                {
                    direction = 1;
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                }
                else
                {
                    direction = -1;
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
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (isInAttack)
            {
                if (InAttackTime <= 0) InAttackTime = 0;
                else InAttackTime -= Time.deltaTime;
                if (direction == 1) rb.velocity = new Vector2(InAttackTime * 1f, 0);
                else rb.velocity = new Vector2(-InAttackTime * 1f, 0);
            }
            if (AttackEnd)
            {
                anim.SetBool("attack", false);
                AttackCD = 0.3f;
            }
            if (AttackCD > 0) AttackCD -= Time.deltaTime;
            else AttackCD = 0;
            if (AttackCD == 0) AttackEnd = false;
        }
        if (Stunned)
        {
            rb.velocity = new Vector2(-StunTime * direction * 1f, rb.velocity.y);
        }
    }
    void Attack()
    {
        if (math.abs(playerTransform.transform.position.x - transform.position.x) <= 1.5f && AttackCD <= 0 && !isAttack)
        {
            direction = playerTransform.transform.position.x > transform.position.x ? 1 : -1;
            isAttack = true;
            anim.SetBool("attack", true);
        }
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
