using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D myFeet;
    private CapsuleCollider2D myCapsule;
    private Animator anim;
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private int dashCount = 1;
    private int direction = 1;
    private bool isOnGround;

    private bool isDash = false;
    private float dashTime = 0.2f;
    private float dashCD = 0;
    private int dashDir = 1;

    private bool isAttack = false;
    private float attackTime = 0.15f;

    public int health;
    protected float immuneTime;

    public LayerMask groundMask;
    public LayerMask magnetMask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myCapsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (immuneTime > 0) immuneTime -= Time.deltaTime;
        if (health <= 0) Destroy(gameObject);

        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (dirX > 0) direction = 1;
        else if (dirX < 0) direction = -1;
        isOnGroundCheck();
        CheckAnimation(dirX);
        if (isOnGround) jumpCount = 2;

        if (Input.GetButtonDown("Dash") && !isDash && dashCount > 0)
        {
            dashCount--;
            dashCD = 0.4f;
            dashTime = 0.2f;
            isDash = true;
            dashDir = direction;
        }
        if (isDash)
        {
            dashTime -= Time.deltaTime;
            rb.velocity = new Vector2(dashDir * moveSpeed * 3.5f, 0);
            if (dashTime <= 0) isDash = false;
        }
        dashCD -= Time.deltaTime;
        if (dashCD <= 0)
        {
            dashCD = 0;
            if (dashCount < 1) dashCount++;
        }

        if (isAttack)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                isAttack = false;
                anim.SetBool("attacking", false);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpCount--;
            if(jumpCount > 0) rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        if (!isDash && Input.GetButtonDown("Fire1") && !isAttack)
        {
            isAttack = true;
            attackTime = 0.27f;
        }

        Debug.Log(health);
    }
    void isOnGroundCheck()
    {
        if (myCapsule.IsTouchingLayers(magnetMask))
        {
            isOnGround = true;
        }
        else
        {
            if (myFeet.IsTouchingLayers(groundMask))
            {
                isOnGround = true;
            }
            else isOnGround = false;
        }
    }

    void CheckAnimation(float dirX)
    {
        if (isDash)
        {
            anim.SetBool("dashing", true);
        }
        else
        {
            anim.SetBool("dashing", false);
        }
        if (dirX != 0)
        {
            if (isOnGround && !isDash)
            {
                anim.SetBool("running", true);
            }
        }
        else
        {
            if (isOnGround)
            {
                anim.SetBool("running", false);
            }
        }
        if (dirX > 0)
        {
            sr.flipX = false;
        }
        if (dirX < 0)
        {
            sr.flipX = true;
        }
        if (!isOnGround)
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
            }
            if (rb.velocity.y > 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
        }

        if (!isDash && Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attacking", true);
        }
    }
    public void GetHit(int getdamage, float immunetime)
    {
        if (immuneTime <= 0)
        {
            immuneTime = immunetime;
            health -= getdamage;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            int damage = Random.Range(45, 56);
            collision.GetComponent<Enemy>().GetHit(damage, 0.2f);
        }
    }
}
