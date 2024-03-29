using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TankAI : Enemy
{
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;
    private Animator anim;

    // time before dash ends
    [SerializeField] private float dashDuration = 2f;
    // time before dash can start
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashSpeed = 2f;

    private Vector2 baseSpeed = new Vector2(6f, 0f);
    private float currDashCooldown;
    private float currDashDuration = 2f;
    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        currDashCooldown = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (!Stunned)
        {
            // start dash when conditions are met
            if (currDashCooldown <= 0 && math.abs(rb.position.x - playerTransform.position.x) <= 6f)
            {
                anim.SetBool("dashing", true);
                isDashing = true;
                rb.velocity = new Vector2(dashSpeed * rb.velocity.x, rb.velocity.y);
                currDashCooldown = dashCooldown;
            }

            // end dash when conditions are met
            if (currDashDuration <= 0)
            {
                anim.SetBool("dashing", false);
                isDashing = false;
                currDashDuration = dashDuration;
            }

            // daaaaaaaash
            if (isDashing)
            {
                currDashDuration -= Time.deltaTime;
            }

            // switch direction to follow player iff not dashing
            else
            {
                if (playerTransform.position.x >= transform.position.x)
                {
                    rb.velocity = new Vector2(baseSpeed.x, rb.velocity.y);
                }
                else rb.velocity = new Vector2(-baseSpeed.x, rb.velocity.y);
                currDashCooldown -= Time.deltaTime;
            }
        }
        if (Stunned)
        {
            rb.velocity = new Vector2(-StunTime * direction * 1f, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // only deal collision damage if tank dashes into player
        if (collision.CompareTag("Player") && isDashing)
        {
            collision.GetComponent<PlayerController>().GetHit(30, 0.2f);
        }

        //stop dashing if tank hits something
        //isDashing = false;
        //currDashDuration = dashDuration;
    }
}