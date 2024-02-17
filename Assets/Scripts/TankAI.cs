using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TankAI : Enemy
{
    private Rigidbody2D rb;
    private CapsuleCollider2D myCapsule;
    private BoxCollider2D myFeet;

    // time before dash ends
    [SerializeField] private float dashDuration = 2f;
    // time before dash can start
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashSpeed = 2f;

    private Vector2 baseSpeed = new Vector2(6f, 0f);
    private float currDashCooldown;
    private float currDashDuration = 0f;
    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myCapsule = GetComponent<CapsuleCollider2D>();
        health = 500;
        currDashCooldown = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (currDashCooldown <= 0) {
            isDashing = true;
            rb.velocity = new Vector2(dashSpeed * rb.velocity.x, baseSpeed.y);
            currDashCooldown = dashCooldown;
        }

        if (currDashDuration <= 0) {
            isDashing = false;
            currDashDuration = dashDuration;
            if (playerTransform.position.x >= transform.position.x) {
                rb.velocity = baseSpeed;
            } else rb.velocity = new Vector2(-baseSpeed.x, baseSpeed.y);
        }

        // daaaaaaaash
        if (isDashing) {
            currDashDuration -= Time.deltaTime;
        }

        // switch direction to follow player iff not dashing
        else {

            currDashCooldown -= Time.deltaTime;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().GetHit(10, 0.2f);
        }
    }
}