using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    //TO DO: UPDATE THIS DAMN THING TO A STATE MACHINE!!!!!!
    void Update()
    {
        if (immuneTime > 0) immuneTime -= Time.deltaTime; //immune time
        if (health <= 0) Destroy(gameObject); //if HP go below 0, destroy the game object(add the special "last breath" in future updates)

        isOnGroundCheck(); //check if the player is on ground
        if (isOnGround) jumpCount = 2; //if on ground, allows double jump

        if (!isDash && Input.GetButtonDown("Fire1") && !isAttack) //if input attack && not dashing && not already attacking)
        {
            isAttack = true;
            attackTime = 0.27f;
        }
        if (isAttack) //if attacking
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                isAttack = false;
                anim.SetBool("attacking", false);
            }
            if (isOnGround) rb.velocity = new Vector2(0, 0); //if attack on ground, set the velocity to 0
        }

        float dirX = Input.GetAxisRaw("Horizontal");
        if (!isAttack) //if not attacking, the player can change direction based on horizontal move input
        {
            if (dirX > 0) direction = 1;
            else if (dirX < 0) direction = -1;
        }
        if (!isAttack || !isOnGround) //if not performing ground attack, the player can move freely (not attacking or jump attacking)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Dash") && dashCount > 0 && !isDash && !isAttack) //if input dashing && not dashing && can dash && is not attacking
        {
            dashCount--;
            dashCD = 0.4f;
            dashTime = 0.2f;
            isDash = true;
            dashDir = direction;
        }
        if (isDash) //if dashing, doing all the stats
        {
            dashTime -= Time.deltaTime;
            rb.velocity = new Vector2(dashDir * moveSpeed * 3.5f, 0);
            if (dashTime <= 0) isDash = false;
        }
        dashCD -= Time.deltaTime;
        if (dashCD <= 0) //calculating dash CD
        {
            dashCD = 0;
            if (dashCount < 1) dashCount++;
        }

        if (Input.GetButtonDown("Jump") && jumpCount > 0 && !isDash && !isAttack) //if input jump && can jump && not dashing or attacking
        {
            jumpCount--;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        CheckAnimation(dirX);
        Debug.Log(health);
    }
    void isOnGroundCheck()
    {
        if (myCapsule.IsTouchingLayers(magnetMask)) //if touching magnets, is on ground
        {
            isOnGround = true;
        }
        else
        {
            if (myFeet.IsTouchingLayers(groundMask)) //if feet is touching ground, is on ground
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
