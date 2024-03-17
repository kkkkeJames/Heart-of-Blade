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
    [SerializeField] private int damage = 50;
    public int direction = 1;
    private bool isOnGround;

    private bool isDash = false;
    private float dashTime = 0.2f;
    private float dashCD = 0;
    private int dashDir = 1;

    private bool isAttack = false;
    private float attackTime = 0.15f;
    private bool isCharge = false;
    private float chargeTime = 0;

    private int comboSerial = 0; //the serial of combo
    private int comboCount = 0; //the count of combo
    private float comboTime = 0; //the duration of combo window

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
    //TO DO: UPDATE THIS THING TO A STATE MACHINE!!!!!!
    void Update()
    {
        if (immuneTime > 0) immuneTime -= Time.deltaTime; //immune time
        if (health <= 0) Destroy(gameObject); //if HP go below 0, destroy the game object(add the special "last breath" in future updates)

        isOnGroundCheck(); //check if the player is on ground
        if (isOnGround) jumpCount = 2; //if on ground, allows double jump

        Attack();
        float dirX = Input.GetAxisRaw("Horizontal");
        Move(dirX);
        LingerSpeed();
        Dash();
        Jump();

        CheckAnimation(dirX);
        //Debug.Log(health);
    }
    private bool isMoveAttack = false;
    void Attack()
    {
        if (!isDash && Input.GetButtonDown("Fire1") && !isAttack) //if input attack && not dashing && not already attacking)
        {
            if (comboTime < 0.55f) Combo(true, false);
            else Combo(false, false);
            //isAttack = true;
            //attackTime = 0.27f;
            //damage = 50;
        }
        if (isAttack) //if attacking
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                comboTime = 0.7f;
                isMoveAttack = false;
                isAttack = false;
                anim.SetBool("attacking", false);
                anim.SetBool("chargeattacking1", false);
                anim.SetBool("chargeattacking2", false);
            }
            if (isOnGround) rb.velocity = new Vector2(0, 0); //if attack on ground, set the velocity to 0
            if (isAttack && isMoveAttack) rb.velocity = new Vector2(direction * 120 * attackTime, 0);
        }
        //if on the ground/not attack or dashing/press down right key, start charging
        if (isOnGround && !isDash && !isAttack && Input.GetButtonDown("Fire2"))
        {
            isCharge = true;
            anim.SetBool("charging", true);
        }
        //when charging, chargeTime increases
        if (isCharge)
        {
            rb.velocity = new Vector2(0, 0);
            chargeTime += Time.deltaTime;
        }
        //if the player already charges for more than 0.25f, and if it charges for more than 4f or release right key, do a charge attack
        if (isCharge && chargeTime >= 0.25f && (chargeTime >= 4f || !Input.GetButton("Fire2")))
        {
            anim.SetBool("charging", false);
            //anim.SetBool("attacking", true);
            Combo(false, true);
            comboTime = 0.8f;
            //anim.SetBool("attacking", true);
            //isCharge = false;
            //isAttack = true;
            //attackTime = 0.27f;
            //damage = (int)(50 * (1 + chargeTime / 2));
            //chargeTime = 0;
        }
        if (!isDash && !isAttack && !isCharge) comboTime -= Time.deltaTime;
        if (!isAttack && comboTime <= 0)
        {
            comboTime = 0;
            comboCount = 0;
            comboSerial = 0;
        }
    }
    void Combo(bool slowed, bool right)
    {
        if (direction == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else transform.localScale = new Vector3(-1, 1, 1);
        if (comboSerial == 0 && slowed)
        {
            if (comboCount == 1) comboSerial = 1;
            if (comboCount == 2) comboSerial = 2;
        }
        if (comboSerial == 0 && right)
        {
            if (comboCount == 0) comboSerial = 3;
            if (comboCount == 1) comboSerial = 4;
            if (comboCount == 2) comboSerial = 5;
        }
        switch (comboSerial)
        {
            case 0: //normal atk -> normal atk -> normal atk
                if (comboCount == 0)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 50;
                    comboCount++;
                    return;
                }
                if (comboCount == 1)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 50;
                    comboCount++;
                    return;
                }
                if (comboCount == 2)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 60;
                    comboSerial = 0;
                    comboCount = 0;
                    return;
                }
                break;
            case 1: //normal atk (pause) -> normal atk -> normal atk -> normal atk
                if (comboCount == 0)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 50;
                    comboCount++;
                    return;
                }
                if (comboCount == 1)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 60;
                    comboCount++;
                    return;
                }
                if (comboCount == 2)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 65;
                    comboCount++;
                    return;
                }
                if (comboCount == 3)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 65;
                    comboSerial = 0;
                    comboCount = 0;
                    return;
                }
                break;
            case 2: //normal atk -> normal atk (pause) -> normal atk -> normal atk -> normal atk
                if (comboCount == 0)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 50;
                    comboCount++;
                    return;
                }
                if (comboCount == 1)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 50;
                    comboCount++;
                    return;
                }
                if (comboCount == 2)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 40;
                    comboCount++;
                    return;
                }
                if (comboCount == 3)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 45;
                    comboCount++;
                    return;
                }
                if (comboCount == 3)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 45;
                    comboSerial = 0;
                    comboCount = 0;
                    return;
                }
                break;
            case 3: //special atk (charge)
                if (comboCount == 0)
                {
                    anim.SetBool("chargeattacking1", true);
                    isCharge = false;
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = (int)(50 * (1 + chargeTime / 2));
                    chargeTime = 0;
                    comboSerial = 0;
                    comboCount = 0;
                    return;
                }
                break;
            case 4: //normal atk -> special atk
                if (comboCount == 0)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 50;
                    comboCount++;
                    return;
                }
                if (comboCount == 1)
                {
                    anim.SetBool("chargeattacking1", true);
                    isAttack = true;
                    isMoveAttack = true;
                    attackTime = 0.27f;
                    damage = 60;
                    comboSerial = 0;
                    comboCount = 0;
                    return;
                }
                break;
            case 5: //normal atk -> normal atk -> special atk (charge)
                if (comboCount == 0)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 50;
                    comboCount++;
                    return;
                }
                if (comboCount == 1)
                {
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = 60;
                    comboCount++;
                    return;
                }
                if (comboCount == 2)
                {
                    anim.SetBool("chargeattacking2", true);
                    isCharge = false;
                    isAttack = true;
                    attackTime = 0.27f;
                    damage = (int)(85 * (1 + chargeTime / 2));
                    chargeTime = 0;
                    comboSerial = 0;
                    comboCount = 0;
                    return;
                }
                break;
        }
    }
    public float lingerSpeedTime = 0f;
    public Vector2 lingerSpeed = Vector2.zero;
    void LingerSpeed()
    {
        if (lingerSpeedTime > 0)
        {
            lingerSpeedTime -= Time.deltaTime;
            rb.velocity = lingerSpeed;
        }
    }
    void Move(float dirX)
    {
        if (!isAttack) //if not attacking, the player can change direction based on horizontal move input
        {
            if (dirX > 0) direction = 1;
            else if (dirX < 0) direction = -1;
        }
        if (!isCharge && (!isAttack || !isOnGround)) //if not performing ground attack/charge, the player can move freely (not attacking or jump attacking)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0 && !isDash && !isAttack && !isCharge) //if input jump && can jump && not dashing or attacking
        {
            jumpCount--;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
    void Dash()
    {
        if (Input.GetButtonDown("Dash") && dashCount > 0 && !isDash && !isAttack && !isCharge) //if input dashing && not dashing && can dash && is not attacking
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (dirX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
        if (collision.CompareTag("Enemy") || collision.CompareTag("EliteEnemy"))
        {
            collision.GetComponent<Enemy>().GetHit(damage, 0.2f);
        }
    }
}
